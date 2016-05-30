using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using System;

namespace RssAgregator.BAL.Services
{
    public class TranslateService : ITranslateService
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        public GenericResult<string> Translate(string text)
        {
            var result = new GenericResult<string>();

            try
            {
                using (var db = new RssAggregatorModelContainer())
                {
                    result.SetDataResult(db.GetTransliteration(text));
                }

                return result;
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
