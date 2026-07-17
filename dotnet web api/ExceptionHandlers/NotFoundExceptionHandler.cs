using Microsoft.AspNetCore.Diagnostics;
using dotnet_web_api.ExceptionHandlers;

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