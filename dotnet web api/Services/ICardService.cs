using dotnet_web_api.Dtos;
using dotnet_web_api.Models;

namespace dotnet_web_api.Services;

public interface ICardService
{
    Task<List<CardResponse>> GetAllCardsAsync();
    Task<CardResponse?> GetCardByIdAsync(int id);
    Task<CardResponse> AddCardAsync(Card card);
    Task<bool> UpdateCardAsync(int id, Card card);
    Task<bool> DeleteCardAsync(int id);
}