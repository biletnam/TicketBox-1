using System.Web.Mvc;
using System.Web.Routing;
using TicketBox.Domain.Entities;
using TicketBox.WebUI.Infrastructure;

namespace TicketBox.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
