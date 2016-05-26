using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RssAgregator.BAL.Services
{
    public class LoggingService : ILoggingService
    {
        private const string DEFAULT_COOKIE_NAME = "userkey";

        private const int COOKIE_EXPIRE_DAYS = 365;

        [Dependency]
        public ISettingService SettingService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }

        public GenericResult<bool> LogFEException(string errorMessage)
        {
            return LogException(errorMessage, LogTypeEnum.FE);
        }

        public GenericResult<bool> LogWEBException(string errorMessage)
        {
            return LogException(errorMessage, LogTypeEnum.WEB);
        }

        public GenericResult<bool> LogUserActivity(LogUserActivityModel userActivityModel, HttpResponseMessage responce, HttpRequestMessage request)
        {
            var result = new GenericResult<bool>();

            try
            {
                var userData = UserService.GetUserData(request);
                if (userData.InfoResult.ResultCode == Models.Enums.ResultCodeEnum.Success)
                {
                    using (var db = new RssAggregatorModelContainer(true))
                    {
                        var userKey = string.Empty;
                        var isNewUser = true;

                        User user = null;
                        
                        var requestCookie = request.Headers.GetCookies(DEFAULT_COOKIE_NAME).FirstOrDefault();
                        if (requestCookie != null)
                        {
                            userKey = requestCookie[DEFAULT_COOKIE_NAME].Value;
                            isNewUser = false;
                        }
                        else
                        {
                            userKey = Guid.NewGuid().ToString();

                            var cookie = new CookieHeaderValue(DEFAULT_COOKIE_NAME, userKey)
                            {
                                Expires = DateTimeOffset.UtcNow.AddDays(COOKIE_EXPIRE_DAYS),
                                Domain = request.RequestUri.Host,
                                Path = "/"
                            };

                            responce.Headers.AddCookies(new[] { cookie });
                        }

                        if (userData.DataResult != null && !string.IsNullOrEmpty(userData.DataResult.UserKey))
                        {
                            user = db.GetEntity<User>(el => el.IsActive && el.UserKey.ToLower() == userData.DataResult.UserKey.ToLower());
                            isNewUser = user == null;
                        }

                        db.AddEntity(new UserActivityLog
                        {
                            Activity = (ActivityEnum)userActivityModel.Activity,
                            Browser = userActivityModel.Browser,
                            BrowserVersion = userActivityModel.BrowserVersion,
                            City = userActivityModel.City,
                            Country = userActivityModel.Country,
                            DateTime = DateTime.UtcNow,
                            Organization = userActivityModel.Organization,
                            Region = userActivityModel.Region,
                            User = user,
                            IsNew = isNewUser,
                            UserKey = userKey
                        });

                        result.SetDataResult(true);
                    }
                }
                else
                {
                    result.SetErrorResultCode(userData);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }

        private GenericResult<bool> LogException(string errorMessage, LogTypeEnum type)
        {
            var result = new GenericResult<bool>();

            try
            {
                Logger.LogException(errorMessage, type);
                result.SetDataResult(true);
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
