using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.Controllers
{
    public class NavController : Controller
    {
        private EFDbContext db = new EFDbContext();

        public PartialViewResult Menu(string category = null)
        {
            IEnumerable<string> categories = db.TypeEvents
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x);

            ViewBag.SelectedCategory = category;

            return PartialView(categories);
        }
    }
}