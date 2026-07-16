namespace dotnet_web_api.Dtos;

public class ColumnResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int BoardId { get; set; }
    public List<CardResponse>? Cards { get; set; } 
}