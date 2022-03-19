using MediatR;

namespace Muvids.Application.Features.Bookings.Commands.CreateBooking;
public class CreateBookingCommand : IRequest<CreateBookingCommandResponse>
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public Guid RoomId { get; set; }
}
