using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ticky.API.Middlewares;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;

        if (exception is ValidationException fluentException)
        {
            problemDetails.Title = "Validation error";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = "One or more validation errors has occurred";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationErrors = fluentException.Errors.Select(x => x.ErrorMessage).ToList();
            problemDetails.Extensions.Add("errors", validationErrors);
        }
        else
        {
            problemDetails.Title = exception.Message;
        }

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
