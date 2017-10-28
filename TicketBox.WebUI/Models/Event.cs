using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketBox.WebUI.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time Event")]
        public DateTime TimeEvent { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Special Event")]
        public byte SpecialEvent { get; set; }
                
        public int TypeEventID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //ссылка на тип мероприятия
        public virtual TypeEvent TypeEvent { get; set; }
        //ссылка на список билетов
        public virtual List<Ticket> Tickets { get; set; }
    }
}
