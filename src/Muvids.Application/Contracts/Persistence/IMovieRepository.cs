using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Domain.Entities;

namespace Muvids.Application.Contracts.Persistence;

public interface IMovieRepository : IAsyncRepository<Movie>
{
}
