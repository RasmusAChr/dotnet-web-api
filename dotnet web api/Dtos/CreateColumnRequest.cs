namespace dotnet_web_api.Dtos;

public class CreateColumnRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int BoardId { get; set; }
}