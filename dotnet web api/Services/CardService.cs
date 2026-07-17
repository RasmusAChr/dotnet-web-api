using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.ExceptionHandlers;
using dotnet_web_api.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class CardService(
    AppDbContext context,
    IValidator<CreateCardRequest> createValidator,
    IValidator<UpdateCardRequest> updateValidator) : ICardService
{
    public async Task<List<CardResponse>> GetAllCardsAsync()
    {
        var cards = await context.Cards.Select(c => new CardResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ColumnId = c.ColumnId
        }).ToListAsync();

        return cards;
    }
        

    public async Task<CardResponse?> GetCardByIdAsync(int id)
    {
        var card = await context.Cards
            .Where(c => c.Id == id)
            .Select(c => new CardResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ColumnId = c.ColumnId
            })
            .FirstOrDefaultAsync();
        
        return card;
    }

    public async Task<CardResponse?> AddCardAsync(CreateCardRequest cardRequest)
    {
        await createValidator.ValidateAndThrowAsync(cardRequest);
        
        var columnExists = await context.Columns.AnyAsync(column => column.Id == cardRequest.ColumnId);
        if (!columnExists)
            return null;
        
        var newCard = new Card
        {
            Name = cardRequest.Name,
            Description = cardRequest.Description,
            ColumnId = cardRequest.ColumnId
        };
        
        context.Cards.Add(newCard);
        await context.SaveChangesAsync();

        return new CardResponse
        {
            Id = newCard.Id,
            Name = newCard.Name,
            Description = newCard.Description,
            ColumnId = newCard.ColumnId
        };

    }

    public async Task<CardResponse?> UpdateCardAsync(int id, UpdateCardRequest cardRequest)
    {
        await updateValidator.ValidateAndThrowAsync(cardRequest);
        
        var cardToUpdate = await context.Cards.FindAsync(id);
        
        // Check if the card is valid
        if (cardToUpdate == null) 
            throw new NotFoundException($"Card with id {id} not found");
        
        // Check if column is valid
        var columnExists = await context.Columns.AnyAsync(column => column.Id == cardRequest.ColumnId);
        if (!columnExists)
            throw new NotFoundException($"Column with id {cardRequest.ColumnId} not found");
        
        cardToUpdate.Name = cardRequest.Name;
        cardToUpdate.Description = cardRequest.Description;
        cardToUpdate.ColumnId = cardRequest.ColumnId;
        
        await context.SaveChangesAsync();

        return new CardResponse
        {
            Id = cardToUpdate.Id,
            Name = cardToUpdate.Name,
            Description = cardToUpdate.Description,
            ColumnId = cardToUpdate.ColumnId
        };
    }

    public async Task<bool> DeleteCardAsync(int id)
    {
        var cardToDelete = await context.Cards.FindAsync(id);
        
        if (cardToDelete == null)
            throw new NotFoundException($"Card with id {id} not found");
        
        context.Cards.Remove(cardToDelete);
        await context.SaveChangesAsync();
        
        return true;
    }
}