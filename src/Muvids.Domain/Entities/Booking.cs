using Muvids.Domain.Common;
using System;

namespace Muvids.Domain.Entities;
public class Booking : AuditableEntity
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public Guid RoomId { get; set; }

    public Room Room { get; set; } = null!;
}

