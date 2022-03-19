using Microsoft.EntityFrameworkCore;
using Muvids.Application.Contracts;
using Muvids.Domain.Common;
using Muvids.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Muvids.Persistence;

public class MuvidsDbContext : DbContext
{
    private readonly ILoggedInUserService _loggedInUserService;

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Room> Room { get; set; }

    public MuvidsDbContext(DbContextOptions<MuvidsDbContext> options) : base(options)
    {
    }

    public MuvidsDbContext(DbContextOptions<MuvidsDbContext> options,
                           ILoggedInUserService loggedInUserService) : base(options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

 
        this._loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MuvidsDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = _loggedInUserService.UserId;
                    entry.Entity.IsDeleted = false;
                    entry.Entity.IsActive =true;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

