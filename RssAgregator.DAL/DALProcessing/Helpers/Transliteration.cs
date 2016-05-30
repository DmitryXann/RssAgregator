using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace RssAgregator.DAL
{
    public partial class RssAggregatorModelContainer : DbContext
    {
        public string GetTransliteration(string text)
        {
            var transliterationData = GetDBSet<Transliteration>().ToList();
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

            var transliterationString = transliterationResult.Aggregate(string.Empty, (agg, el) => agg + el);
            var rgx = new Regex("[^a-zA-Z0-9_ ]");

            return rgx.Replace(transliterationString, string.Empty);
        }
    }
}
