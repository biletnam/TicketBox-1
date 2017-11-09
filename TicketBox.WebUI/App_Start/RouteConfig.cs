using System.Web.Mvc;
using System.Web.Routing;

namespace TicketBox.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(null,
            //    "",
            //    new
            //    {
            //        controller = "Home",
            //        action = "List",
            //        category = (string)null
            //    }
            //);

            routes.MapRoute(null,
                "{category}",
                new { controller = "Home", action = "List" }
            );

            routes.MapRoute(null, "{controller}/{action}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
