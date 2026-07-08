using dotnet_web_api.Models;

namespace dotnet_web_api.Services;

public interface ICardService
{
    Task<List<Card>> GetAllCardsAsync();
    Task<Card?> GetCardByIdAsync(int id);
    Task<Card> AddCardAsync(Card card);
    Task<bool> UpdateCardAsync(int id, Card card);
    Task<bool> DeleteCardAsync(int id);
}