using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssAgregator.BAL.Services
{
    public class NewsService : INewsService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        [Dependency]
        public ITranslateService TranslateService { get; set; }

        public GenericResult<IEnumerable<NewsModel>> GetNews(int pageSize, int pageNumber, bool hideAdult)
        {
            var result = new GenericResult<IEnumerable<NewsModel>>();

            try
            {
                if (pageSize > 0 && pageNumber > 0)
                {
                    using (var db = new RssAggregatorModelContainer())
                    {
                        result.SetDataResult(db.GetDBSet<News>(el => el.IsActive)
                                                .OrderBy(el => el.Id)
                                                .Skip(pageSize * (pageNumber - 1))
                                                .Take(pageSize)
                                                .ToList()
                                                .Select(el => el.GetModel())
                                                .ToList());

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

        public GenericResult<bool> AddNewsItem(NewsItemModel inputParams)
        {
            var result = new GenericResult<bool>();

            try
            {
                Func<string, string, string> preparePostid = (postName, userName) =>
                {
                    var dateTimeNow = DateTime.Now;
                    return string.Format("{0}_{1}_{2}",
                                                    postName.Replace(' ', '_'),
                                                    userName,
                                                    string.Format("{0}.{1}.{2}_{3}:{4}:{5}.{6}", (dateTimeNow.Day < 10 ? "0" + dateTimeNow.Day.ToString() : dateTimeNow.Day.ToString()),
                                                                                         (dateTimeNow.Month < 10 ? "0" + dateTimeNow.Month.ToString() : dateTimeNow.Month.ToString()),
                                                                                         dateTimeNow.Year.ToString().Substring(2),
                                                                                         dateTimeNow.Hour, dateTimeNow.Minute, dateTimeNow.Second, dateTimeNow.Millisecond));
                };

                using (var db = new RssAggregatorModelContainer(true))
                {
                    var user = db.GetEntity<User>(el => el.Id == inputParams.UserId && el.IsActive);
                    var userName = user.Name.ToLower();

                    var translatePost = TranslateService.Translate(inputParams.PostName);
                    var postId = preparePostid(translatePost.InfoResult.ResultCode == Models.Enums.ResultCodeEnum.Success 
                                                    ? translatePost.DataResult 
                                                    : Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}{1}", userName, DateTime.Now.Ticks))), 
                                                userName);

                    var systemDataSource = db.GetEntity<DataSources>(el => el.Type == DataSourceEnum.System);

                    db.AddEntity<News>(new News {
                        PostId = postId,
                        AuthorId = userName,
                        AuthorName = userName,
                        AuthorLink = string.Format("{0}/{1}", systemDataSource.Uri.TrimEnd(new[] { '/' }), userName),
                        PostName = inputParams.PostName,
                        PostLink = string.Format("{0}/{1}", systemDataSource.Uri.TrimEnd(new[] { '/' }), userName),
                        PostContent = inputParams.PostContent,
                        PostTags = inputParams.PostTags,
                        IsActive = true,
                        AdultContent = inputParams.AdultContent,

                        DataSource = systemDataSource,
                        User = user
                    });
                    result.SetDataResult(true);

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
