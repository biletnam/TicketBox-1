namespace TicketBox.WebUI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }        
        public string Place { get; set; }
        public bool Delivery { get; set; }
        public decimal Price { get; set; }

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
