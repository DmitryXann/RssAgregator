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
                using (var db = new RssAggregatorModelContainer(true))
                {
                    var user = db.GetEntity<User>(el => el.Id == inputParams.UserId && el.IsActive);
                    var userName = user.Name.ToLower();

                    var systemDataSource = db.GetEntity<DataSources>(el => el.Type == DataSourceEnum.System);

                    db.AddEntity<News>(new News {
                        PostId = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}{1}", userName, DateTime.Now.Ticks))),
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
