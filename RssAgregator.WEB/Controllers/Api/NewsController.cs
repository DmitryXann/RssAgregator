﻿using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB.Controllers.Api
{
    public class NewsController : ApiController
    {
        [Dependency]
        public INewsService NewsService { get; set; }

        [System.Web.Http.HttpGet]
        public JsonResult NewsSearch([FromUri] NewsSearchModel id)
        {
            return new JsonResult
            {
                Data = NewsService.GetNews(id.PageSize, id.PageNumber, id.HideAdult),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
