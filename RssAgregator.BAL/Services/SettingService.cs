using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using System;

namespace RssAgregator.BAL.Services
{
    public class SettingService : ISettingService
    {
        public GenericResult<string> GetSetting(string key)
        {
            var result = new GenericResult<string>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetEntity<Settings>(el => el.Key == key).Value);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
            }

            return result;
        }

        public GenericResult<string> GetUserFriendlyExceptionMessage()
        {
            return GetSetting("ExceptionOccured");
        }
    }
}
