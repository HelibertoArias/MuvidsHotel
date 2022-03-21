namespace Muvids.Application.Features.Bookings.Query.GetBookingsList;

public class BookingDto
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}