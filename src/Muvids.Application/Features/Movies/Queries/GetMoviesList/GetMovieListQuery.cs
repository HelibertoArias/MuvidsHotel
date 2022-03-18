using MediatR;
using Muvids.Application.Helpers;

namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class GetMovieListQuery : IRequest<List<MovieListVm>>
{
    const int MAX_PAGE_SIZE = 20;
    
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }
}
