﻿using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface INewsService
    {
        GenericResult<IEnumerable<NewsModel>> GetNews(int pageSize, int pageNumber, bool hideAdult);

        GenericResult<bool> AddNewsItem(NewsItemModel inputParams);
    }
}
