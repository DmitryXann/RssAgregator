using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Net.Http;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ILoggingService
    {
        GenericResult<bool> LogFEException(string errorMessage);

        GenericResult<bool> LogWEBException(string errorMessage);

        GenericResult<bool> LogUserActivity(LogUserActivityModel userActivityModel, HttpResponseMessage responce, HttpRequestMessage request);
    }
}
