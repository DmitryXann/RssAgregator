using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace RssAgregator.BAL.Services
{
    public class UserService : IUserService
    {
        private const string DEFAULT_COOKIE_NAME = "userdata";

        private const int COOKIE_EXPIRE_DAYS = 365;

        [Dependency]
        public ISettingService SettingService { get; set; }

        public GenericResult<bool> UserNameExists(string name)
        {
            var result = new GenericResult<bool>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetEntity<User>(el => el.Name.ToLower() == name.ToLower()) != null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<bool> EmailExists(string email)
        {
            var result = new GenericResult<bool>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetEntity<User>(el => el.Email.ToLower() == email.ToLower()) != null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<string> Login(UserModel userModel, HttpResponseMessage responce, HttpRequestMessage request)
        {
            var result = new GenericResult<string>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    var user = db.GetEntity<User>(el => el.IsActive && el.Name.ToLower() == userModel.Name.ToLower() && el.Password.ToLower() == userModel.Password.ToLower());
                    if (user != null)
                    {
                        result.SetDataResult(user.Name);

                        var cookie = new CookieHeaderValue(DEFAULT_COOKIE_NAME, user.UserKey)
                        {
                            Domain = request.RequestUri.Host,
                            Path = "/"
                        };

                        if (userModel.CreateCookie.HasValue && userModel.CreateCookie.Value)
                        {
                            cookie.Expires = DateTimeOffset.UtcNow.AddDays(COOKIE_EXPIRE_DAYS);
                        }

                        responce.Headers.AddCookies(new[] { cookie });
                    }
                    else
                    {
                        result.SetWarningResultCode(SettingService.GetSetting("LOGIN_IncorrectUserNameOrPassword"));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<bool> LogOut(HttpResponseMessage responce, HttpRequestMessage request)
        {
            var result = new GenericResult<bool>();

            try
            {
                var cookie = new CookieHeaderValue(DEFAULT_COOKIE_NAME, string.Empty)
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Domain = request.RequestUri.Host,
                    Path = "/"
                };

                responce.Headers.AddCookies(new[] { cookie });

                result.SetDataResult(true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<string> CreateUpdate(UserModel userModel, HttpResponseMessage responce, HttpRequestMessage request)
        {
            var result = new GenericResult<string>();

            try
            {
                Func<string, string> getHashSum = (source) =>
                {
                    using (var md5Hash = MD5.Create())
                    {
                        return md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source)).Aggregate(string.Empty, (agg, el) => agg + el.ToString("x2"));
                    }
                };

                using (var db = new RssAggregatorModelContainer(true))
                {
                    var user = new User();

                    if (userModel.Exists.HasValue && userModel.Exists.Value)
                    {
                        var requestCookie = request.Headers.GetCookies(DEFAULT_COOKIE_NAME).FirstOrDefault();
                        if (requestCookie != null)
                        {
                            var userKey = requestCookie[DEFAULT_COOKIE_NAME].Value;
                            if (!string.IsNullOrEmpty(userKey))
                            {
                                user = db.GetEntity<User>(el => el.IsActive && el.UserKey.ToLower() == userKey.ToLower() && el.Name.ToLower() == userModel.Name.ToLower());
                                if (user != null)
                                {
                                    user.Email = userModel.Email.ToLower();
                                    user.Password = userModel.Password.ToLower();
                                    user.UserKey = getHashSum(string.Format("{0}{1}", userModel.Name.ToLower(), userModel.Password.ToLower()));

                                    result.SetDataResult(userModel.Name);
                                }
                                else
                                {
                                    Logger.LogException(string.Format("Can`t find user by UserKey, Name: {0}, UserKey: {1}", userModel.Name, userKey), LogTypeEnum.BAL);
                                    result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
                                }
                            }
                            else
                            {
                                Logger.LogException(string.Format("Can`t find UserKey in cookies, Name: {0}", userModel.Name), LogTypeEnum.BAL);
                                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
                            }
                        }
                        else
                        {
                            Logger.LogException(string.Format("Can`t find UserKey in cookies, Name: {0}", userModel.Name), LogTypeEnum.BAL);
                            result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
                        }
                    }
                    else
                    {
                        var userNameExists = db.GetEntity<User>(el => el.IsActive && el.Name.ToLower() == userModel.Name.ToLower());
                        if (userNameExists != null)
                        {
                            result.SetErrorResultCode(SettingService.GetSetting("LOGIN_UserNameAlreadyExists"));
                        }
                        else
                        {
                            user.Name = userModel.Name;
                            user.Email = userModel.Email;
                            user.Password = userModel.Password.ToLower();
                            user.IsActive = true;
                            user.UserKey = getHashSum(string.Format("{0}{1}", userModel.Name.ToLower(), userModel.Password.ToLower()));
                            user.Type = UserTypeEnum.User;

                            db.AddEntity(user);
                        }
                        
                        result.SetDataResult(userModel.Name);
                    }

                    if (!string.IsNullOrEmpty(result.DataResult))
                    {
                        var cookie = new CookieHeaderValue(DEFAULT_COOKIE_NAME, user.UserKey)
                        {
                            Domain = request.RequestUri.Host,
                            Path = "/"
                        };

                        if (userModel.CreateCookie.HasValue && userModel.CreateCookie.Value) {
                            cookie.Expires = DateTimeOffset.UtcNow.AddDays(COOKIE_EXPIRE_DAYS);
                        }
                        
                        responce.Headers.AddCookies(new[] { cookie });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        public GenericResult<UserModel> GetUserData(HttpRequestMessage request)
        {
            var result = new GenericResult<UserModel>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    var requestCookie = request.Headers.GetCookies(DEFAULT_COOKIE_NAME).FirstOrDefault();
                    if (requestCookie != null)
                    {
                        var userKey = requestCookie[DEFAULT_COOKIE_NAME].Value;
                        if (!string.IsNullOrEmpty(userKey))
                        {
                            var user = db.GetEntity<User>(el => el.IsActive && el.UserKey.ToLower() == userKey.ToLower());
                            if (user != null)
                            {
                                result.SetDataResult(user.GetModel());
                            }
                            else
                            {
                                result.SetDataResult((UserModel)null);
                            }
                        }
                        else
                        {
                            result.SetDataResult((UserModel)null);
                        }
                    }
                    else
                    {
                        result.SetDataResult((UserModel)null);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }
    }
}
