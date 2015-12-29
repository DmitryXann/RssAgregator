using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class ProstoPleerController : ApiController
    {
        [Dependency]
        public IProstoPleerService ProstoPleerService { get; set; }

        [System.Web.Http.HttpGet]
        public async Task<JsonResult> GetSongs([FromUri] ProstoPleerSearchModel id)
        {
            return new JsonResult
            {
                Data = await ProstoPleerService.Search(id),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
