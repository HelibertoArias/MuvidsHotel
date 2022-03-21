using Muvids.Application.Responses;

namespace Muvids.Application.Features.Bookings.Query.AvailableBookings;

public class CheckRoomAvailabilityQueryResponse : BaseResponse
{
    public AvailableBookingDto Booking { get; set; } = null!;

}