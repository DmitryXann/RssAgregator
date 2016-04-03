using RssAgregator.Models.Results;
using System.Collections.Generic;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface ISettingService
    {
        GenericResult<string> GetSetting(string key);

        GenericResult<IDictionary<string, string>> GetAllUISettings();

        GenericResult<string> GetUserFriendlyExceptionMessage();
    }
}
