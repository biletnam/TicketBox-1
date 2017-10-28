using System;
using System.Collections.Generic;
using System.Data;
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
    public class TicketController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Ticket
        public async Task<ActionResult> Index()
        {
            var tickets = db.Tickets.Include(d => d.TypeTicket);
            return View(await tickets.ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = await db.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            ViewBag.TypeTicketId = new SelectList(db.TypeTickets, "TypeTicketId", "Name");
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Name");
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TicketId, Place, Delivery, Price, TypeTicketID, EventID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeTicketId = new SelectList(db.TypeTickets, "TypeEventId", "Name", ticket.TypeTicketID);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Name");
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeTicketId = new SelectList(db.TypeTickets, "TypeTicketId", "Name", ticket.TypeTicketID);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Place", "Delivery", "Price", "TypeTicketID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ticketToUpdate = await db.Tickets.FindAsync(id);
            if (ticketToUpdate == null)
            {
                Ticket deletedTicket = new Ticket();

                TryUpdateModel(deletedTicket, fieldsToBind);

                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");

                ViewBag.TypeTicketId = new SelectList(db.TypeTickets, "TypeTicketId", "Name", deletedTicket.TypeTicketID);
                return View(deletedTicket);
            }

            if (TryUpdateModel(ticketToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(ticketToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Ticket)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Ticket)databaseEntry.ToObject();

                        if (databaseValues.Place != clientValues.Place)
                            ModelState.AddModelError("Place", "Current value: "
                                + databaseValues.Place);
                        if (databaseValues.Delivery != clientValues.Delivery)
                            ModelState.AddModelError("Delivery", "Current value: "
                                + String.Format("{0:c}", databaseValues.Delivery));
                        if (databaseValues.Price != clientValues.Price)
                            ModelState.AddModelError("TimeEvent", "Current value: "
                                + String.Format("{0:d}", databaseValues.Price));
                        if (databaseValues.TypeTicketID != clientValues.TypeTicketID)
                            ModelState.AddModelError("TypeTicketID", "Current value: "
                                + db.TypeEvents.Find(databaseValues.TypeTicketID).Name);                        

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        ticketToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.TypeTicketId = new SelectList(db.TypeTickets, "TypeTicketId", "Name", ticketToUpdate.TypeTicketID);
            return View(ticketToUpdate);
        }

        // GET: Ticket/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
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

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Ticket ticket)
        {
            try
            {
                db.Entry(ticket).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = ticket.TicketId });
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(ticket);
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