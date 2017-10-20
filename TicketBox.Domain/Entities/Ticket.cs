using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public TypeTicket Type { get; set; }
        public int Place { get; set; }
        public string Delivery { get; set; }
        public Event Event { get; set; }
        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        //public Ticket()
        //{
        //    ApplicationUsers = new List<ApplicationUser>();
        //}
    }
}
