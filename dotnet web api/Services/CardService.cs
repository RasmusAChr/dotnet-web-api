using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class CardService(AppDbContext context) : ICardService
{
    public async Task<List<CardResponse>> GetAllCardsAsync()
        => await context.Cards.Select(c => new CardResponse
        {
            Name = c.Name,
            Description = c.Description,
        }).ToListAsync();

    public async Task<CardResponse?> GetCardByIdAsync(int id)
    {
        var card = await context.Cards
            .Where(c => c.Id == id)
            .Select(c => new CardResponse
            {
                Name = c.Name,
                Description = c.Description
            })
            .FirstOrDefaultAsync();
        
        
        
        return card;
    }

    public async Task<CardResponse> AddCardAsync(Card card)
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