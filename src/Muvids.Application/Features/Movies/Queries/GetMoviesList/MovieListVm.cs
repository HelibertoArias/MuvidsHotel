namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class MovieListVm
{
    public Guid Id { get; set; }
    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string Language { get; set; } = null!;
}
