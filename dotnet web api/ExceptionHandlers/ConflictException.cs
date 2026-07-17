namespace dotnet_web_api.ExceptionHandlers;

public class ConflictException(string message) : Exception(message);