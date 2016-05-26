using System.Web;

namespace RssAgregator.WEB.Helpers.Extensions
{
    public static class BrowserCompatibilityExtension
    {
        public static bool BrowserIsSupported(this HttpRequestBase request)
        {
            var result = false;

            switch (request.Browser.Browser.ToLower())
            {
                case "chrome":
                case "edge":
                case "firefox":
                    result = true;
                break;
                case "internetexplorer":
                    result = request.Browser.MajorVersion >= 11;
                break;
                case "safari":
                    result = request.Browser.MajorVersion >= 5 && request.Browser.MinorVersion >= 1;
                break;
            }
          
            return result;
        }
    }
}