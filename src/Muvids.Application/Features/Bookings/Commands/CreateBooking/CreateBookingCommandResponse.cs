using Muvids.Application.Responses;

namespace Muvids.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandResponse : BaseResponse
{
    public CreateBookingDto Booking { get; set; } = null!;
}