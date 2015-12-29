using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.RawUrl.EndsWith("/"))
            {
                return View();
            }
            return Redirect(Request.RawUrl + "/");
        }
    }
}
