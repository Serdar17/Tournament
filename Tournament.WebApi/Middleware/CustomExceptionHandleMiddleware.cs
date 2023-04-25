using System.Net;
using System.Text.Json;
using FluentValidation;
using Tournament.Application.Common.Exceptions;
using Tournament.Domain.Shared;

namespace Tournament.Middleware;

public class CustomExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception.InnerException)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = HandleFailure(validationException);
                break; 
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { error = notFoundException.Message });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }

    private string HandleFailure(ValidationException validationException)
    {
        var failures = validationException.Errors
            .Select(e => new Error(
                e.PropertyName,
                e.ErrorMessage))
            .Distinct()
            .ToArray();
        var validationResult = ValidationResult.WithErrors(failures, "Validation Error");
        return JsonSerializer.Serialize(validationResult);
    }
}