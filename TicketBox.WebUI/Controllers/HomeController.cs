using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBox.WebUI.Models;
using TicketBox.WebUI.ViewModel;

namespace TicketBox.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private int pageSize = 4;
        private EFDbContext context = new EFDbContext();

        public ViewResult List(string category, int page = 1)
        {
            EventsListViewModel model = new EventsListViewModel
            {
                Events = context.Events
                                    .Where(p => category == null || p.Type.Name == category)
                                    .OrderBy(game => game.EventId)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        context.Events.Count() :
                        context.Events.Where(x => x.Type.Name == category).Count()
                },
                CurrentCategory = context.TypeEvents.FirstOrDefault(x => x.Name == category)
            };
            return View(model);
        }
    }
}