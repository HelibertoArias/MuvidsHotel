using MediatR;

namespace Muvids.Application.Features.Bookings.Commands.CancelBooking;
public class CancelBookingCommand : IRequest
{
    public Guid Id { get; set; }
}
