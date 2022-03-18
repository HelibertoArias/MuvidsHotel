using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Application.Exceptions;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Movies.Commands.UpdateMovie;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Movie> _movieRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public UpdateMovieCommandHandler(IMapper mapper,
                                    IAsyncRepository<Movie> movieRepository,
                                    ILoggedInUserService loggedInUserService)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this._loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
    }
    public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {

        var movieToUpdate = await _movieRepository.GetByIdAsync(request.Id);
        if (movieToUpdate == null)
        {
            throw new NotFoundException(nameof(Movie), request.Id);
        }


        var validator = new UpdateMovieCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult);
        }

        if (movieToUpdate.CreatedBy != _loggedInUserService.UserId)
        {
            throw new BadRequestException("You can not update this movie. It belongs to other user.");
        }


        var movie = _mapper.Map(request, movieToUpdate, typeof(UpdateMovieCommand), typeof(Movie));
        await _movieRepository.UpdateAsync(movieToUpdate);
        return Unit.Value;

    }
}
