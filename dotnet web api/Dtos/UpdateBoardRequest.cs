namespace dotnet_web_api.Dtos;

public class UpdateBoardRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}