using Muvids.Domain.Common;
using System.Collections.Generic;

namespace Muvids.Domain.Entities;

public class Room : AuditableEntity
{
    public string Name { get; set; } = null!;

    public IList<Booking> Bookings { get; set; } = null!;
}