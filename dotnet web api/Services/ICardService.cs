using dotnet_web_api.Dtos;
using dotnet_web_api.Models;

namespace dotnet_web_api.Services;

public interface ICardService
{
    Task<List<CardResponse>> GetAllCardsAsync();
    Task<CardResponse?> GetCardByIdAsync(int id);
    Task<CardResponse> AddCardAsync(CreateCardRequest card);
    Task<CardResponse> UpdateCardAsync(int id, UpdateCardRequest card);
    Task<bool> DeleteCardAsync(int id);
}