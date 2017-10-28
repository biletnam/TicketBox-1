using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TicketBox.WebUI.App_Start;
using TicketBox.WebUI.Infrastructure;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
