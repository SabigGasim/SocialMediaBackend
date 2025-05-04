using SocialMediaBackend.Modules.Users.Domain.Common.Exceptions;
using System.Text.Json;

namespace SocialMediaBackend.Modules.Users.Api.Middlewares;

internal class ExceptionStatusCodeMiddleware(RequestDelegate next, IHostEnvironment environment)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            int statusCode = ex switch
            {
                BusinessRuleValidationException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                context.Response.StatusCode,
                ex.Message,
                Details = environment.IsProduction() ? ex.Details : null
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);

        }
        catch (ApplicationException)
        {
            throw;
        }
    }
}

