using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Muvids.Persistence;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Muvids.Web.API.IntegrationTest.Base;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }

    public HttpClient GeAuthenticatedClient()
    {
        //return CreateClient();
        HttpClient client = WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            });
        }).CreateClient();

        return client;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthHandler.DefaultScheme;
                options.DefaultScheme = TestAuthHandler.DefaultScheme;
            }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                   TestAuthHandler.DefaultScheme, options => { });
        });

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<MuvidsDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<MuvidsDbContext>(options =>
            {
                options.UseInMemoryDatabase("MuvidsInMemoryDbForTesting");
            });


            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<MuvidsDbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            }
        });
    }


    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string DefaultScheme = "Test";
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "Test user"), new Claim(ClaimTypes.NameIdentifier, "Jane") };
            var identity = new ClaimsIdentity(claims, DefaultScheme);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, DefaultScheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

}
