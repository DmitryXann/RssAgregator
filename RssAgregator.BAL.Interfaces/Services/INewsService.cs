using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface INewsService
    {
        GenericResult<IEnumerable<NewsModel>> GetNews(int pageSize, int pageNumber, bool hideAdult);

        GenericResult<NewsModel> GetNewsItem(string newsItemId);

        GenericResult<bool> AddEditNewsItem(NewsItemModel inputParams);

        GenericResult<IEnumerable<KeyValuePair<string, int>>> GetAllNewsTags();
    }
}
