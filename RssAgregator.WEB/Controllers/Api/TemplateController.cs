using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class TemplateController : ApiController
    {
        [Dependency]
        public ITemplateService TemplateService { get; set; }

        [System.Web.Http.HttpGet]
        public JsonResult GetTemplate(string id)
        {
            var userId = 1;
            return new JsonResult
            {
                Data = TemplateService.GetUserTemplate(id, userId),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
