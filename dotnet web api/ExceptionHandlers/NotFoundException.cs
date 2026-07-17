namespace dotnet_web_api.ExceptionHandlers;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}