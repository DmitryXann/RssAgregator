using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using System;

namespace RssAgregator.BAL.Services
{
    public class LoggingService : ILoggingService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        public GenericResult<bool> LogFEException(string errorMessage)
        {
            return LogException(errorMessage, LogTypeEnum.FE);
        }

        public GenericResult<bool> LogWEBException(string errorMessage)
        {
            return LogException(errorMessage, LogTypeEnum.FE);
        }

        private GenericResult<bool> LogException(string errorMessage, LogTypeEnum type)
        {
            var result = new GenericResult<bool>();

            try
            {
                Logger.LogException(errorMessage, type);
                result.SetDataResult(true);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogTypeEnum.BAL);
                result.SetErrorResultCode(SettingService.GetUserFriendlyExceptionMessage());
            }

            return result;
        }
    }
}
