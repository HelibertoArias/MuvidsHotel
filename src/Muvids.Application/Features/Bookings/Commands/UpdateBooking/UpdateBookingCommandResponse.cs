using Muvids.Application.Responses;

namespace Muvids.Application.Features.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandResponse : BaseResponse
{
    public UpdateBookingDto Booking { get; set; } = null!;
}