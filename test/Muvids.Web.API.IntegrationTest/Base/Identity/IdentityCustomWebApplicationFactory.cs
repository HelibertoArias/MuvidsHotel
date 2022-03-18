using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muvids.Identity;
using Muvids.Persistence;
using System;
using System.Linq;
using System.Net.Http;

namespace Muvids.Web.API.IntegrationTest.Base.Identity;

public class IdentityCustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<MuvidsIdentityDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<MuvidsIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("MuvidsIdentityDbContextInMemoryDbForTesting");
            });




            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<MuvidsIdentityDbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();

                try
                {
                    IdentityUtilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            }
        });
    }
}
