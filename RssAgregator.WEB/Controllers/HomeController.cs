using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public ISettingService SettingService { get; set; }

        public ActionResult Index()
        {
            if (Request.RawUrl.EndsWith("/"))
            {
                return View(SettingService.GetAllUISettings());
            }

            return Redirect(Request.RawUrl + "/");
        }
    }
}
