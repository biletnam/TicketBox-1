using System.Collections.Generic;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Abstract
{
    public interface ITypeEventRepository
    {
        IEnumerable<TypeEvent> TypeEvents { get; }
    }
}
