using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TicketBox.WebUI.Models;
using TicketBox.WebUI.ViewModel;

namespace TicketBox.WebUI.Controllers
{
    public class CartController : Controller
    {
        private EFDbContext db = new EFDbContext();

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int eventId, string returnUrl)
        {
            Event _event = db.Events
                .FirstOrDefault(g => g.EventId == eventId);

            if (_event != null)
            {
                cart.AddItem(_event, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int eventId, string returnUrl)
        {
            Event _event = db.Events
                .FirstOrDefault(g => g.EventId == eventId);

            if (_event != null)
            {
                cart.RemoveLine(_event);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}
