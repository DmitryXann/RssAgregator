using RssAgregator.Models.Results;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ISettingService
    {
        GenericResult<string> GetSetting(string key);

        GenericResult<string> GetUserFriendlyExceptionMessage();
    }
}
