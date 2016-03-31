using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RssAgregator.WEB
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{inputParams}",
                defaults: new { inputParams = RouteParameter.Optional }
            );
        }
    }
}
