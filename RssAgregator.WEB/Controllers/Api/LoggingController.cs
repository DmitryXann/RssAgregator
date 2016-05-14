using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.WEB.Models;
using System.Web.Http;
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
                Data = LoggingService.LogFEException(string.Format("Exception: {0}, UserAgent: {1}", inputParams.ErrorMsg, Request.Headers.UserAgent)),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}