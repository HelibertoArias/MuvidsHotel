using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreateMovieCommandResponse>

{
    private readonly IMapper _mapper;

    private readonly IAsyncRepository<Movie> _movieRepository;

    public CreateMovieCommandHandler(IMapper mapper,
                                    IAsyncRepository<Movie> movieRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }
    public async Task<CreateMovieCommandResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var createMovieCommandResponse = new CreateMovieCommandResponse();

        var validator = new CreateMovieCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createMovieCommandResponse.Success = false;
            createMovieCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createMovieCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (createMovieCommandResponse.Success)
        {
            var movie = _mapper.Map<Movie>(request);
            movie = await _movieRepository.AddAsync(movie);
            createMovieCommandResponse.Movie = _mapper.Map<CreateMovieDto>(movie);
        }

        return createMovieCommandResponse;

    }
}
