using Microsoft.AspNetCore.Diagnostics;

namespace dotnet_web_api.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFoundException)
            return false;

        await Results.Problem(
            statusCode: 404,
            detail: notFoundException.Message
        ).ExecuteAsync(httpContext);
        
        return true;
    }
}