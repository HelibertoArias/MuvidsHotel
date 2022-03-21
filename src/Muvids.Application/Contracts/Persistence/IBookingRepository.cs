using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Domain.Entities;

namespace Muvids.Application.Contracts.Persistence;

public interface IBookingRepository : IAsyncRepository<Booking>
{
    Task<bool> HasBookingConflictsAsync(Booking booking);

    Task<IReadOnlyList<Booking>> ListAllByUserAsync(string userId);
}