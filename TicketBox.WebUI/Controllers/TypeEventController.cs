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
    public class TypeEventController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: TypeEvent
        public async Task<ActionResult> Index()
        {
            var events = db.TypeEvents;
            return View(await events.ToListAsync());
        }

        // GET: TypeEvent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeEvent typeEvent = await db.TypeEvents.FindAsync(id);

            if (typeEvent == null)
            {
                return HttpNotFound();
            }
            return View(typeEvent);
        }

        // GET: TypeEvent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeEvent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeEventId, Name")] TypeEvent typeEvent)
        {
            if (ModelState.IsValid)
            {
                db.TypeEvents.Add(typeEvent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeEvent);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeEvent typeEvent = db.TypeEvents.Find(id);

            if (typeEvent == null)
            {
                return HttpNotFound();
            }
            return View(typeEvent);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var typeEvent = db.TypeEvents.Find(id);

            if (TryUpdateModel(typeEvent, "", new string[] { "Name" }))
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
            return View(typeEvent);
        }

        // GET: TypeEvent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeEvent typeEvent = db.TypeEvents.Find(id);
            if (typeEvent == null)
            {
                return HttpNotFound();
            }
            return View(typeEvent);
        }

        // POST: TypeEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeEvent typeEvent = db.TypeEvents.Find(id);
            db.TypeEvents.Remove(typeEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}