using System.Collections.Generic;
using System.Linq;

namespace RssAgregator.Models.Services.OnlineRadio
{
    public class OnlineRadioTypeAhedJsonContentModel
    {
        public Dictionary<string, int> content_auto { get; set; }

        public IEnumerable<string> GetAllKeys()
        {
            return content_auto.Select(el => el.Key);
        }
    }
}
