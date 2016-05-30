using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using RssAgregator.WEB.Models;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class LoggingController : ApiController
    {
        [Dependency]
        public ILoggingService LoggingService { get; set; }

        [System.Web.Http.HttpPost]
        public JsonResult LogFEError([FromBody]LoggingModel inputParams)
        {
            return new JsonResult
            {
                Data = LoggingService.LogFEException(string.Format("Exception: {0}, Browser: {1}, Version: {2}", inputParams.ErrorMsg, HttpContext.Current.Request.Browser.Browser, HttpContext.Current.Request.Browser.MajorVersion)),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpPost]
        [ResponseType(typeof(JsonResult))]
        public HttpResponseMessage LogUserActivity([FromBody]LogUserActivityModel inputParams)
        {
            inputParams.Browser = HttpContext.Current.Request.Browser.Browser;
            inputParams.BrowserVersion = HttpContext.Current.Request.Browser.MajorVersion;

            var dataResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var responce = Request.CreateResponse(HttpStatusCode.OK, dataResult);

            dataResult.Data = LoggingService.LogUserActivity(inputParams, responce, Request);

            return responce;
        }
    }
}