using System.ComponentModel.DataAnnotations;

namespace dotnet_web_api.Models;

public class Column
{
    public int Id { get; set; }
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    [StringLength(200)]
    public string? Description { get; set; }
    public int BoardId { get; set; }
    public Board? Board { get; set; }
    public IList<Card> Cards { get; } = new List<Card>();
}