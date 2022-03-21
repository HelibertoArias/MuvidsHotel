using MediatR;

namespace Muvids.Application.Features.Bookings.Query.AvailableBookings;
public class CheckRoomAvailabilityQuery : IRequest<CheckRoomAvailabilityQueryResponse>
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}
