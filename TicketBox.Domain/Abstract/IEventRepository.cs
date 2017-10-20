﻿using System.Collections.Generic;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }
    }
}