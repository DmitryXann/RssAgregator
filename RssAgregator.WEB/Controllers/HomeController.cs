using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.WEB.Helpers.Extensions;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers
{
    public class HomeController : Controller
    {
        private const string BROWSER_NOT_SUPPORTED_ACTION_NAME = "NotSupportedBrowser";
        private const string IGNORE_BROWSER_NOT_SUPPORTED_ACTION_NAME = "IgnoreNotSupportedBrowser";
        private const string ERROR_ACTION_NAME = "Error";

        [Dependency]
        public ISettingService SettingService { get; set; }

        public ActionResult Index(string expectedAction)
        {
            ActionResult result = Redirect("/");

            if (string.IsNullOrEmpty(expectedAction) || expectedAction.ToLower() == IGNORE_BROWSER_NOT_SUPPORTED_ACTION_NAME.ToLower())
            {
                if (Request.BrowserIsSupported() || (!string.IsNullOrEmpty(expectedAction) && expectedAction.ToLower() == IGNORE_BROWSER_NOT_SUPPORTED_ACTION_NAME.ToLower()))
                {
                    if (Request.RawUrl.EndsWith("/"))
                    {
                        result = View(SettingService.GetAllUISettings());
                    }
                    else
                    {
                        result = Redirect(Request.RawUrl + "/");
                    }
                }
                else
                {
                    result = Redirect(string.Format("/{0}", BROWSER_NOT_SUPPORTED_ACTION_NAME));
                }
            }
            else if (expectedAction.ToLower() == BROWSER_NOT_SUPPORTED_ACTION_NAME.ToLower())
            {
                result = View(BROWSER_NOT_SUPPORTED_ACTION_NAME);
            }
            else if (expectedAction.ToLower() == ERROR_ACTION_NAME.ToLower())
            {
                result = View(ERROR_ACTION_NAME);
            }

            return result;
        }
    }
}
