using Muvids.Application.Contracts.Persistence;
using Muvids.Domain.Entities;
using Muvids.Persistence.Repositories.Common;

namespace Muvids.Persistence.Repositories;

public class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    public MovieRepository(MuvidsDbContext dbContext) : base(dbContext)
    {
    }
}
