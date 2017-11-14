using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperKidCity
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "api/School",
                url: "api/{controller}/{action}/{id}",
                defaults: new { controller = "SchoolApi", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SuperKidCity.Areas.API.Controllers" }
            );

            routes.MapRoute(
                name: "api/Parent",
                url: "api/{controller}/{action}/{id}",
                defaults: new { controller = "ParentApi", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SuperKidCity.Areas.API.Controllers" }
            );

            routes.MapRoute(
                name: "School",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "School", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SuperKidCity.Controllers" }
            );
            routes.MapRoute(
                name: "Parent",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Parent", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SuperKidCity.Controllers" }
            );
        }
    }
}
