using System.Collections.Generic;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.ViewModel
{
    public class EventsListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public TypeEvent CurrentCategory { get; set; }
    }
}