using RssAgregator.Models.Results;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ILoggingService
    {
        GenericResult<bool> LogFEException(string errorMessage);

        GenericResult<bool> LogWEBException(string errorMessage);
    }
}
