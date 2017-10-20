using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }        
        public string Place { get; set; }
        public string Delivery { get; set; }

        //ссылка на мероприятие
        public Event Event { get; set; }
        //ссылка на тип билета
        public TypeTicket Type { get; set; }

        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        //public Ticket()
        //{
        //    ApplicationUsers = new List<ApplicationUser>();
        //}
    }
}
