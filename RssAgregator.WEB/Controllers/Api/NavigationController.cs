using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class NavigationController : ApiController
    {
        [Dependency]
        public INavigationService NavigationService { get; set; }

        [System.Web.Http.HttpGet]
        public JsonResult GetNavigationData()
        {
            return new JsonResult
            {
                Data = NavigationService.GetNavigationData(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}