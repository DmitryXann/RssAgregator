using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface INewsService
    {
        IEnumerable<NewsModel> GetNews(int pageSize, int pageNumber, bool hideAdult);
    }
}
