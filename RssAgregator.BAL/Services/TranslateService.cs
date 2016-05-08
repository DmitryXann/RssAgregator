using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.DAL;
using RssAgregator.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                    var transliterationData = db.GetDBSet<Transliteration>().ToList();
                    var transliterationResult = new List<string>();

                    foreach (var charEl in text.ToLower().Where(el => char.IsLetterOrDigit(el) || el == ' ' || el == '\t' || el == '_'))
                    {
                        var expectedEl = transliterationData.FirstOrDefault(el => el.FromLetter.Contains(charEl));
                        if (expectedEl != null)
                        {
                            transliterationResult.Add(expectedEl.ToLetter);
                        }
                        else
                        {
                            transliterationResult.Add(charEl.ToString());
                        }
                    }

                    var transliterationString= transliterationResult.Aggregate(string.Empty, (agg, el) => agg + el);
                    var rgx = new Regex("[^a-zA-Z0-9_ ]");

                    result.SetDataResult(rgx.Replace(transliterationString, string.Empty));
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
