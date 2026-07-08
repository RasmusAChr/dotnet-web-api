namespace dotnet_web_api.Models;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    // Maybe labels in future
}