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



        //modelBuilder.Entity<Movie>().HasData(
        //    new Movie()
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "Inception",
        //        ReleaseYear = 2010,
        //        Rating = "PG-13",
        //        Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
        //        IsPublic = true,
        //        CreatedBy = "00000000-0000-0000-0000-000000000000",
        //        CreatedDate = DateTime.Now,
        //        LastModifiedBy = "00000000-0000-0000-0000-000000000000",
        //        LastModifiedDate = DateTime.Now
        //    });

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

