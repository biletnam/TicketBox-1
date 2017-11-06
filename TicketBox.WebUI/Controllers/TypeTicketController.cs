using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.Controllers
{
    public class TypeTicketController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: TypeTicket
        public async Task<ActionResult> Index()
        {
            var typeTickets = db.TypeTickets;
            return View(await typeTickets.ToListAsync());
        }

        // GET: TypeTicket/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeTicket typeTicket = await db.TypeTickets.FindAsync(id);

            if (typeTicket == null)
            {
                return HttpNotFound();
            }
            return View(typeTicket);
        }

        // GET: TypeTicket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeTicket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeTicketId, Name")] TypeTicket typeTicket)
        {
            if (ModelState.IsValid)
            {
                db.TypeTickets.Add(typeTicket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeTicket);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeTicket typeTicket = db.TypeTickets.Find(id);

            if (typeTicket == null)
            {
                return HttpNotFound();
            }
            return View(typeTicket);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var typeTicket = db.TypeTickets.Find(id);

            if (TryUpdateModel(typeTicket, "", new string[] { "Name" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(typeTicket);
        }

        // GET: TypeEvent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeTicket typeTicket = db.TypeTickets.Find(id);
            if (typeTicket == null)
            {
                return HttpNotFound();
            }
            return View(typeTicket);
        }

        // POST: TypeEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeTicket typeTicket = db.TypeTickets.Find(id);
            db.TypeTickets.Remove(typeTicket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}