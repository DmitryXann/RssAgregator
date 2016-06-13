using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System;
using System.Net.Http;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ILoggingService
    {
        GenericResult<bool> LogFEException(string errorMessage, string browserData);

        GenericResult<bool> LogWEBException(string errorMessage, string browserData);

        GenericResult<bool> LogWEBException(Exception ex);

        GenericResult<bool> LogUserActivity(LogUserActivityModel userActivityModel, HttpResponseMessage responce, HttpRequestMessage request);
    }
}
