using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Muvids.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        //services.AddTransient<IYourInterface, IYourClass>();
        return services;
    }
}

