using System.ComponentModel.DataAnnotations;

namespace dotnet_web_api.Models;

public class Card
{
    public int Id { get; set; }
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    [StringLength(200)]
    public string? Description { get; set; } = string.Empty;
    public int ColumnId  { get; set; }
    public Column? Column { get; set; }
    // Maybe labels in future
}