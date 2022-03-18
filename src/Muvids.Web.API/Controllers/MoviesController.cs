using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Features.Movies.Commands.CreateMovie;
using Muvids.Application.Features.Movies.Commands.DeleteMovie;
using Muvids.Application.Features.Movies.Commands.UpdateMovie;
using Muvids.Application.Features.Movies.Queries.GetMoviesList;

namespace Muvids.Web.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMediator mediator,
                            ILogger<MoviesController> logger)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("all", Name = "GetAllMovies")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieListVm>))]
    public async Task<IActionResult> GetAllMovies([FromQuery] GetMovieListQuery movieListQuery)
    {
        var dtos = await _mediator.Send(movieListQuery);
        return Ok(dtos);
    }

    // Get started with Swashbuckle : https://bit.ly/34Vs4jF
    [HttpPost("createmovie", Name = "Create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateMovieDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieCommand data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }

    [HttpPut("updatemovie", Name = "Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieCommand data)
    {
        await _mediator.Send(data);
        return NoContent();
    }

    [HttpPut("deletemovie", Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMovie([FromBody] DeleteMovieCommand data)
    {
        await _mediator.Send(data);
        return NoContent();
    }
}
