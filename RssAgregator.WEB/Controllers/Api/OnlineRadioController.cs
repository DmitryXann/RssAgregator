using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
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
        public async Task<JsonResult> GetSongs([FromUri] OnlineRadioServiceSearchModel inputParams)
        {
            return new JsonResult
            {
                Data = await OnlineRadioService.Search(inputParams),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpGet]
        public async Task<JsonResult> GetTypeAhedResult([FromUri] OnlineRadioServiceSearchModel inputParams)
        {
            return new JsonResult
            {
                Data = await OnlineRadioService.TypeAheadSearch(inputParams.Question),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpPost]
        public JsonResult AddNotPlayebleSong([FromBody]OnlineRadioNotPlayebleSongModel inputParams)
        {
            return new JsonResult
            {
                Data = OnlineRadioService.AddNotPlayebleSong(inputParams),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
