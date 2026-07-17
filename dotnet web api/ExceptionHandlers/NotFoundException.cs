namespace dotnet_web_api.ExceptionHandlers;

public class NotFoundException(string message) : Exception(message);