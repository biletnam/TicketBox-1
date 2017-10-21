using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Event _event, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Event.EventId == _event.EventId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Event = _event,
                    Tickets = _event.Tickets.FindAll(x => x.Event == _event),
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Event _event)
        {
            lineCollection.RemoveAll(l => l.Event.EventId == _event.EventId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Tickets.Sum(x => x.Price) * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; }
        public int Quantity { get; set; }
    }
}
