namespace Muvids.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingDto
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

}
