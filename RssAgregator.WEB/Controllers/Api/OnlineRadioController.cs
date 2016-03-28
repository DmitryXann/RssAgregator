using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using RssAgregator.Models.Services.OnlineRadio;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class OnlineRadioController : ApiController
    {
        [Dependency]
        public IOnlineRadioService OnlineRadioService { get; set; }

        [System.Web.Http.HttpGet]
        public async Task<JsonResult> GetSongs([FromUri] OnlineRadioServiceSearchModel id)
        {
            return new JsonResult
            {
                Data = await OnlineRadioService.Search(id),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
