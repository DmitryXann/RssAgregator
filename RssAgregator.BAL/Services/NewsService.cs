using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
                                                .Select(el => el.GetModel()));

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
