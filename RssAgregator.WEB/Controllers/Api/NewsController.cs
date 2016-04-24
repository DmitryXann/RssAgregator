﻿using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.Models.Services;
using RssAgregator.WEB.Models;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class NewsController : ApiController
    {
        [Dependency]
        public INewsService NewsService { get; set; }

        [System.Web.Http.HttpGet]
        public JsonResult NewsSearch([FromUri] NewsSearchModel inputParams)
        {
            return new JsonResult
            {
                Data = NewsService.GetNews(inputParams.PageSize, inputParams.PageNumber, inputParams.HideAdult),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public JsonResult AddNewsItem([FromBody] NewsItemModel inputParams)
        {
            inputParams.ModificationDate = DateTime.Now;
            inputParams.UserId = 1; //TODO: remove when user login implemented
            return new JsonResult
            {
                Data = NewsService.AddNewsItem(inputParams),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
