using dotnet_web_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotnet_web_api.Services;

public class CardService : ICardService
{
    static List<Card> cards = new()
    {
        new Card { Id = 0, Name = "test1", Description = "This is test card one." },
        new Card { Id = 1, Name = "test2", Description = "This is test card two." },
        new Card { Id = 2, Name = "test3", Description = "This is test card three." },
        new Card { Id = 3, Name = "test4", Description = "This is test card four." }
    };
    
    public async Task<List<Card>> GetAllCardsAsync()
        => await Task.FromResult(cards);

    public async Task<Card?> GetCardByIdAsync(int id)
    {
        var card = cards.FirstOrDefault(c => c.Id == id);
        return await Task.FromResult(card);
    }

    public Task<Card> AddCardAsync(Card card)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCardAsync(int id, Card card)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCardAsync(int id)
    {
        throw new NotImplementedException();
    }
}