using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, List<MovieListVm>>
{
    private readonly IMapper _mapper;

    private readonly IAsyncRepository<Movie> _movieRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public GetMovieListQueryHandler(IMapper mapper,
                                    IAsyncRepository<Movie> movieRepository,
                                    ILoggedInUserService loggedInUserService)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this._loggedInUserService = loggedInUserService;
    }

    public async Task<List<MovieListVm>> Handle(GetMovieListQuery request,
                                                        CancellationToken cancellationToken)
    {

        var eventsFiltered = (await _movieRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize))
                                .ToList() // TODO
                                .Where(x => x.IsPublic || x.CreatedBy == _loggedInUserService.UserId)
                                .OrderBy(x => x.Title);


        
        return _mapper.Map<List<MovieListVm>>(eventsFiltered);
    }
}
