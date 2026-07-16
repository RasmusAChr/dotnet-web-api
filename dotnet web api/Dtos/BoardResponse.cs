using dotnet_web_api.Models;

namespace dotnet_web_api.Dtos;

public class BoardResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<ColumnResponse>? Columns { get; set; }
}