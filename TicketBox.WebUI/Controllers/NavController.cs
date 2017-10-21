using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBox.Domain.Abstract;

namespace TicketBox.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ITypeEventRepository typeEventRepository;

        public NavController(ITypeEventRepository repo)
        {
            typeEventRepository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = typeEventRepository.TypeEvents
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}