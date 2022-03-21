using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Features.Bookings.Commands.CancelBooking;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;
using Muvids.Application.Features.Bookings.Commands.UpdateBooking;
using Muvids.Application.Features.Bookings.Query;
using Muvids.Application.Features.Bookings.Query.GetBookingsList;

namespace Muvids.Web.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IMediator mediator,
                            ILogger<BookingController> logger)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("getbookings", Name = "getbookings")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBookingCommandResponse))]
    public async Task<IActionResult> GetBookings([FromBody] GetBookingListQuery data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }

    [HttpPost("createbooking", Name = "createbooking")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBookingCommandResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }    
    
    [HttpPut("updatebooking", Name = "updatebooking")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateBookingCommandResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBooking([FromBody]  UpdateBookingCommand data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }

    [HttpPut("cancelbooking", Name = "cancelbooking")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand data)
    {
        await _mediator.Send(data);
        return NoContent();
    }


}
