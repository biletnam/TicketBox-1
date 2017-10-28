using System.Linq;
using System.Web.Mvc;
using TicketBox.WebUI.Models;
using TicketBox.WebUI.ViewModel;

namespace TicketBox.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context = new EFDbContext();

        public ViewResult List(string category)
        {
            EventsListViewModel model = new EventsListViewModel
            {
                Events = context.Events
                                    .Where(p => category == null || p.TypeEvent.Name == category)
                                    .OrderBy(game => game.EventId),                
                CurrentCategory = context.TypeEvents.FirstOrDefault(x => x.Name == category)
            };

            if(model.Events.ToList().Count ==  0)
            {
                return View("notFound");
            }
            return View(model);
        }
    }
}