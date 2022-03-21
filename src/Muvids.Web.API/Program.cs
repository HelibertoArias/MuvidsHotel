using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Muvids.Application;
using Muvids.Application.Contracts;
using Muvids.Identity;
using Muvids.Identity.Models;
using Muvids.Infrastructure;
using Muvids.Persistence;
using Muvids.Persistence.Data;
using Muvids.Web.API.Configurations;
using Muvids.Web.API.Helpers;
using Muvids.Web.API.Middleware;
using Muvids.Web.API.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
// Add services to the container.
builder.Services.AddHttpClient();
IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddIdentityServices(configuration);

builder.Services.Configure<GeneralSettings>(configuration.GetSection("GeneralSettings"));


builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(setupAction => // This handle error by modelbinder or the API can handle.
{
    setupAction.InvalidModelStateResponseFactory = context =>
    {
        // create a problem details object
        var problemDetailsFactory = context.HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();
        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                context.HttpContext,
                context.ModelState);

        // add additional info not added by default
        problemDetails.Detail = "See the errors field for details.";
        problemDetails.Instance = context.HttpContext.Request.Path;

        // find out which status code to use
        var actionExecutingContext =
              context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

        // if there are modelstate errors & all keys were correctly
        // found/parsed we're dealing with validation errors
        //
        // if the context couldn't be cast to an ActionExecutingContext
        // because it's a ControllerContext, we're dealing with an issue 
        // that happened after the initial input was correctly parsed.  
        // This happens, for example, when manually validating an object inside
        // of a controller action.  That means that by then all keys
        // WERE correctly found and parsed.  In that case, we're
        // thus also dealing with a validation error.
        if (context.ModelState.ErrorCount > 0 &&
            (context is ControllerContext ||
             actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
        {
            //problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            problemDetails.Title = "One or more validation errors occurred.";

            return new UnprocessableEntityObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        }

        // if one of the keys wasn't correctly found / couldn't be parsed
        // we're dealing with null/unparsable input
        problemDetails.Status = StatusCodes.Status400BadRequest;
        problemDetails.Title = "One or more errors on input occurred.";
        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };
    };
}); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();


// START Swagger
// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
// https://dotnetthoughts.net/openapi-support-for-aspnetcore-minimal-webapi/
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {

                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,


                        },
                        new List<string>()
                      }
                    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Muvids Management API",
        Description = "An ASP.NET Core Web API for managin Movies",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Heliberto Arias",
            Url = new Uri("https://localhost/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://localhost/license")
        }
    });

    // c.OperationFilter<FileResultContentTypeOperationFilter>();
});
// END Swagger

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MuvidsDbContext>();
        // Relational-specific methods can only be used when the context is using a relational database provider

        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
            await Seed.SeedBookings(context);
        }
    }

}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Muvids API");
    });

    app.UseDeveloperExceptionPage();
}

// https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
app.UseCustomExceptionHandler();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    var userManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();

    await Muvids.Identity.Seed.CreateFirstUser.SeedAsync(userManager);
    Log.Information("Application Starting");
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occured while starting the application");
   
}

app.Run();


// https://stackoverflow.com/questions/69058176/how-to-use-webapplicationfactory-in-net6-without-speakable-entry-point
//<ItemGroup> < InternalsVisibleTo Include = "Muvids.Web.API.IntegrationTest" /> </ ItemGroup >
public partial class Program { } // so you can reference it from tests