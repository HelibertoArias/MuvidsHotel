using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Application.Exceptions;
using Muvids.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Muvids.Application.Features.Movies.Commands.DeleteMovie;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Movie> _movieRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public DeleteMovieCommandHandler(IMapper mapper,
                                     IAsyncRepository<Movie> movieRepository,
                                     ILoggedInUserService loggedInUserService)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this._loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
    }

    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new BadRequestException(nameof(request));

        var movieToDelete = await _movieRepository.GetByIdAsync(request.Id);

        if (movieToDelete == null)
        {
            throw new NotFoundException(nameof(Movie), request.Id);
        } 

        if(movieToDelete.CreatedBy != _loggedInUserService.UserId)
        {
            throw new BadRequestException("You can not delete this movie. It belongs to other user.");
        }

        await _movieRepository.DeleteAsync(movieToDelete);
        return Unit.Value;
        
    }
}
