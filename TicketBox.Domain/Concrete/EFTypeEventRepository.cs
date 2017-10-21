using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
{
    public class IFTypeEventRepository : ITypeEventRepository
    {
        EFDbContext typeEventontext = new EFDbContext();

        public IEnumerable<TypeEvent> TypeEvents
        {
            get { return typeEventontext.TypeEvents; }
        }
    }    
}
