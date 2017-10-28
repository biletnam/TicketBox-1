using System.ComponentModel.DataAnnotations;

namespace TicketBox.WebUI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Display(Name = "Delivery")]
        public bool Delivery { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public int TypeTicketID { get; set; }

        public int EventID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //ссылка на мероприятие
        public virtual Event Event { get; set; }
        //ссылка на тип билета
        public virtual TypeTicket TypeTicket { get; set; }
        
    }
}
