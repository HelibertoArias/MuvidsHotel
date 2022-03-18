using Muvids.Application.Exceptions;
using Muvids.Identity.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Muvids.Web.API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.ValdationErrors);
                break;
            case BadRequestException badRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                //result = badRequestException.Message;
                break;
            case NotFoundException notFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case RegisterUserException registerUserException:
                httpStatusCode = HttpStatusCode.BadRequest;

                
                break;
            case Exception ex:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = ex.Message;
                break;

        }

        context.Response.StatusCode = (int)httpStatusCode;

        if (result == string.Empty)
        {
            result = JsonConvert.SerializeObject(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}