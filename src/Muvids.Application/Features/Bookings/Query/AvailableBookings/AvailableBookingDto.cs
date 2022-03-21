namespace Muvids.Application.Features.Bookings.Query.AvailableBookings;

public class AvailableBookingDto
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsAvailable { get; set; }
}