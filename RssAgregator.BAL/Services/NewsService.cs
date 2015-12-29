using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Services;
using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.BAL.Services
{
    public class NewsService : INewsService
    {
        public IEnumerable<NewsModel> GetNews(int pageSize, int pageNumber, bool hideAdult)
        {
            IEnumerable<NewsModel> result = null;

            if (pageSize > 0 && pageNumber > 0)
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result = db.GetDBSet<News>(el => el.IsActive)
                            .OrderBy(el => el.Id)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToList()
                            .Select(el => el.GetModel());
                }
            }

            return result;
        }
    }
}
