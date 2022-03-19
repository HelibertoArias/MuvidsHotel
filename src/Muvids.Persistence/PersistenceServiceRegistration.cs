using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muvids.Application.Contracts.Persistence;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Persistence.Repositories;
using Muvids.Persistence.Repositories.Common;
using System;

namespace Muvids.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        var stringConnection = "MuvidsHotelConnectionString";
        var connectionConfiguration = configuration.GetConnectionString(stringConnection);
        if (connectionConfiguration == null)
        {
            throw new ArgumentNullException(nameof(connectionConfiguration), $"{stringConnection} doesn't exist in your appsetings.json");
        }

        services.AddDbContext<MuvidsDbContext>(options =>
            options.UseSqlServer(connectionConfiguration)
        );


        // Base repository
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        // Other repositories
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();


        return services;
    }
}