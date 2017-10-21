using System.Collections.Generic;
using TicketBox.Domain.Entities;

namespace TicketBox.WebUI.Models
{
    public class EventsListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public TypeEvent CurrentCategory { get; set; }
    }
}