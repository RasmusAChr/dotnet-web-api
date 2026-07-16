namespace dotnet_web_api.Dtos;

public class CardResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ColumnId { get; set; }
}