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
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }).ToListAsync();

    public async Task<CardResponse?> GetCardByIdAsync(int id)
    {
        var card = await context.Cards
            .Where(c => c.Id == id)
            .Select(c => new CardResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .FirstOrDefaultAsync();
        
        return card;
    }

    public async Task<CardResponse> AddCardAsync(CreateCardRequest card)
    {
        var newCard = new Card
        {
            Name = card.Name,
            Description = card.Description
        };
        
        context.Cards.Add(newCard);
        await context.SaveChangesAsync();

        return new CardResponse
        {
            Id = newCard.Id,
            Name = newCard.Name,
            Description = newCard.Description
        };

    }

    public async Task<bool> UpdateCardAsync(int id, UpdateCardRequest card)
    {
        // Check if it's the same card
        if (card.Id != id)
            return false;
        
        var cardToUpdate = await context.Cards.FindAsync(id);
        
        // Check if the card is valid
        if (cardToUpdate == null) 
            return false;
        
        cardToUpdate.Name = card.Name;
        cardToUpdate.Description = card.Description;
        
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCardAsync(int id)
    {
        var cardToDelete = await context.Cards.FindAsync(id);
        
        if (cardToDelete == null)
            return false;
        
        context.Cards.Remove(cardToDelete);
        await context.SaveChangesAsync();
        
        return true;
    }
}