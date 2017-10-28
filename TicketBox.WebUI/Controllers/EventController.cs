using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketBox.WebUI.Models;
using TicketBox.WebUI.ViewModel;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Event
        public async Task<ActionResult> Index()
        {
            var events = db.Events.Include(d => d.Type);
            return View(await events.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event _event = await db.Events.FindAsync(id);

            if (_event == null)
            {
                return HttpNotFound();
            }
            return View(_event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.TypeEvents, "TypeEventId", "Name");
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EventID,Name,Location,TimeEvent,Description,SpecialEvent,TypeID")] Event _event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(_event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.TypeEvents, "TypeEventId", "Name", _event.TypeID);
            return View(_event);
        }

        // GET: Event/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event _event = await db.Events.FindAsync(id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.TypeEvents, "TypeEventId", "Name", _event.TypeID);
            return View(_event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Name", "Location", "TimeEvent", "Description", "SpecialEvent", "TypeID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eventToUpdate = await db.Events.FindAsync(id);
            if (eventToUpdate == null)
            {
                Event deletedEvent = new Event();

                TryUpdateModel(deletedEvent, fieldsToBind);

                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");

                ViewBag.TypeID = new SelectList(db.TypeEvents, "TypeEventId", "Name", deletedEvent.TypeID);
                return View(deletedEvent);
            }

            if (TryUpdateModel(eventToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(eventToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Event)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Event)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", "Current value: "
                                + databaseValues.Name);
                        if (databaseValues.Location != clientValues.Location)
                            ModelState.AddModelError("Location", "Current value: "
                                + String.Format("{0:c}", databaseValues.Location));
                        if (databaseValues.TimeEvent != clientValues.TimeEvent)
                            ModelState.AddModelError("TimeEvent", "Current value: "
                                + String.Format("{0:d}", databaseValues.TimeEvent));
                        if (databaseValues.TypeID != clientValues.TypeID)
                            ModelState.AddModelError("TypeID", "Current value: "
                                + db.TypeEvents.Find(databaseValues.TypeID).Name);
                        if (databaseValues.Description != clientValues.Description)
                            ModelState.AddModelError("Description", "Current value: "
                                + String.Format("{0:d}", databaseValues.Description));
                        if (databaseValues.SpecialEvent != clientValues.SpecialEvent)
                            ModelState.AddModelError("SpecialEvent", "Current value: "
                                + String.Format("{0:d}", databaseValues.SpecialEvent));

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        eventToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.TypeID = new SelectList(db.TypeEvents, "TypeEventId", "Name", eventToUpdate.TypeID);
            return View(eventToUpdate);
        }

        // GET: Event/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event _event = await db.Events.FindAsync(id);
            if (_event == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(_event);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Event _event)
        {
            try
            {
                db.Entry(_event).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = _event.EventId });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(_event);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}