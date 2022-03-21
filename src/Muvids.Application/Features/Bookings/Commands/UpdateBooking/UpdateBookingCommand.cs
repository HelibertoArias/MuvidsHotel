using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Muvids.Application.Features.Bookings.Commands.UpdateBooking;
public class UpdateBookingCommand : IRequest<UpdateBookingCommandResponse>
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}
