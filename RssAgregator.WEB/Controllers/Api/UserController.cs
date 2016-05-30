using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using RssAgregator.WEB.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class UserController : ApiController
    {
        [Dependency]
        public IUserService UserService { get; set; }

        [System.Web.Http.HttpPost]
        [ResponseType(typeof(JsonResult))]
        public HttpResponseMessage Login([FromBody]UserModel inputParams)
        {
            var dataResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var responce = Request.CreateResponse(HttpStatusCode.OK, dataResult);

            dataResult.Data = UserService.Login(inputParams, responce, Request);

            return responce;
        }

        [System.Web.Http.HttpGet]
        [ResponseType(typeof(JsonResult))]
        public HttpResponseMessage LogOut()
        {
            var dataResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var responce = Request.CreateResponse(HttpStatusCode.OK, dataResult);

            dataResult.Data = UserService.LogOut(responce, Request);

            return responce;
        }

        [System.Web.Http.HttpPost]
        [ResponseType(typeof(JsonResult))]
        public HttpResponseMessage CreateUpdate([FromBody]UserModel inputParams)
        {
            var dataResult = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            var responce = Request.CreateResponse(HttpStatusCode.OK, dataResult);

            dataResult.Data = UserService.CreateUpdate(inputParams, responce, Request);

            return responce;
        }

        [System.Web.Http.HttpPost]
        public JsonResult UserNameExists([FromBody]SingleParamPostDataModel inputParams)
        {
            return new JsonResult
            {
                Data = UserService.UserNameExists(inputParams.InputParams),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpPost]
        public JsonResult EmailExists([FromBody]SingleParamPostDataModel inputParams)
        {
            return new JsonResult
            {
                Data = UserService.EmailExists(inputParams.InputParams),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpGet]
        public JsonResult GetUserData()
        {
            var dataResult = UserService.GetUserData(Request);

            if (dataResult.DataResult != null)
            {
                dataResult.DataResult.RemovePrivateData();
            }

            return new JsonResult
            {
                Data = dataResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}