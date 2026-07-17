using Microsoft.AspNetCore.Diagnostics;

namespace dotnet_web_api.ExceptionHandlers;

public class ConflictExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ConflictException conflictException)
            return false;

        await Results.Problem(
            statusCode: 409,
            detail: conflictException.Message
        ).ExecuteAsync(httpContext);
        
        return true;
    }
}