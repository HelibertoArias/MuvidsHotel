using Microsoft.EntityFrameworkCore;
using Muvids.Application.Contracts.Persistence;
using Muvids.Domain.Entities;
using Muvids.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muvids.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(MuvidsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IReadOnlyList<Booking>> ListAllAsync()
    {
        return await _dbContext.Bookings
                    .Where(booking => !booking.IsDeleted && booking.IsActive)
                    .ToListAsync();
    }

    public async Task<IReadOnlyList<Booking>> ListAllByUserAsync(string userId)
    {
        return (await ListAllAsync())
                    .Where(booking => booking.CreatedBy == userId)
                    .ToList();                     
    }

    public async Task<bool> HasBookingConflictsAsync(Booking booking)
    {
        var bookings = await ListAllAsync();

        if(booking.Id != Guid.Empty)
        {
            bookings = bookings.Where(x=>x.Id != booking.Id).ToList();
        }
        


        var result = new List<DateTime>();


        foreach (var item in bookings)
        {
            int days = (item.End - item.Start).Days + 1;

            var dates = Enumerable.Range(0, days).Select(x => item.Start.AddDays(x)).ToList();

            if (dates.Contains(booking.Start) || dates.Contains(booking.End))
            {
                return true;
            }
            
        }

        return false;
    }
}