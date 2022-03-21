using Muvids.Application.Responses;

namespace Muvids.Application.Features.Bookings.Query.GetBookingsList;

public class GetBookingListQueryResponse : BaseResponse
{
    public List<BookingDto> Bookings { get; set; } = null!;
}