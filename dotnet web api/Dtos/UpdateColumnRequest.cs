namespace dotnet_web_api.Dtos;

public class UpdateColumnRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int BoardId { get; set; }
}