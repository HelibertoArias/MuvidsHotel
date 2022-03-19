using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Features.Bookings.Commands.CreateBooking;

namespace Muvids.Web.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;

    public BookingController(IMediator mediator,
                            ILogger<MoviesController> logger)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Get started with Swashbuckle : https://bit.ly/34Vs4jF
    [HttpPost("createbooking", Name = "createbooking")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBookingCommandResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }
}
