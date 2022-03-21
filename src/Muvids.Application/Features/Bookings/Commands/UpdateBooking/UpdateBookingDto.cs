namespace Muvids.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingDto
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}