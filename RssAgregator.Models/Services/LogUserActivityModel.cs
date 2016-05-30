using RssAgregator.Models.Enums;

namespace RssAgregator.Models.Services
{
    public class LogUserActivityModel
    {
        public ActivityEnum Activity { get; set; }

        public string Browser { get; set; }

        public int BrowserVersion { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Organization { get; set; }
    }
}
