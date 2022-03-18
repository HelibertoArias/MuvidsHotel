using Muvids.Application.Responses;

namespace Muvids.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieCommandResponse : BaseResponse
{
    public CreateMovieCommandResponse() : base() { }

    public CreateMovieDto Movie { get; set; } = null!;
}
