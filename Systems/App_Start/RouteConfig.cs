using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Oesia_Arequipa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
               defaults: new { controller = "Login", action = "CerrarSesion", id = UrlParameter.Optional }
            //  defaults: new { controller = "Pagina", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
