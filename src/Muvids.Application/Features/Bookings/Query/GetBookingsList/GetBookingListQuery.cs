using MediatR;

namespace Muvids.Application.Features.Bookings.Query.GetBookingsList;
public class GetBookingListQuery : IRequest<GetBookingListQueryResponse>
{
    // Nothing here, we will be using the user in session
}
