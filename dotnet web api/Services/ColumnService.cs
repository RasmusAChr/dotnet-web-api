using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.ExceptionHandlers;
using dotnet_web_api.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class ColumnService(
    AppDbContext context,
    IValidator<CreateColumnRequest> createValidator,
    IValidator<UpdateColumnRequest> updateValidator) : IColumnService
{
    public async Task<List<ColumnResponse>> GetAllColumnsAsync()
    {
        var columns = await context.Columns.Select(c => new ColumnResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            BoardId = c.BoardId,
            Cards = c.Cards.Select(card => new CardResponse
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description,
                ColumnId = card.ColumnId
            }).ToList()
        }).ToListAsync();
        
        return columns;
    }

    public async Task<ColumnResponse?> GetColumnByIdAsync(int id)
    {
        var column = await context.Columns
            .Where(c => c.Id == id)
            .Select(c => new ColumnResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                BoardId = c.BoardId,
                Cards = c.Cards.Select(card => new CardResponse
                {
                    Id = card.Id,
                    Name = card.Name,
                    Description = card.Description,
                    ColumnId = card.ColumnId
                }).ToList()
            })
            .FirstOrDefaultAsync();
        
        return column;
    }

    public async Task<ColumnResponse> AddColumnAsync(CreateColumnRequest columnRequest)
    {
        await createValidator.ValidateAndThrowAsync(columnRequest);
        
        var boardExists = await context.Boards.AnyAsync(b => b.Id == columnRequest.BoardId);
        if (!boardExists)
            throw new NotFoundException($"Board with id {columnRequest.BoardId} not found");
        
        var newColumn = new Column
        {
            Name = columnRequest.Name,
            Description = columnRequest.Description,
            BoardId = columnRequest.BoardId
        };

        context.Columns.Add(newColumn);
        await context.SaveChangesAsync();

        return new ColumnResponse
        {
            Id = newColumn.Id,
            Name = newColumn.Name,
            Description = newColumn.Description,
            BoardId = newColumn.BoardId
        };
    }

    public async Task<ColumnResponse> UpdateColumnAsync(int id, UpdateColumnRequest columnRequest)
    {
        await updateValidator.ValidateAndThrowAsync(columnRequest);
        
        var boardExists = await context.Boards.AnyAsync(b => b.Id == columnRequest.BoardId);
        if (!boardExists)
            throw new NotFoundException($"Board with id {columnRequest.BoardId} not found");
        
        var columnToUpdate = await context.Columns.FindAsync(id);
        if (columnToUpdate == null)
            throw new NotFoundException($"Column with id {id} not found");
        
        columnToUpdate.Name = columnRequest.Name;
        columnToUpdate.Description = columnRequest.Description;
        columnToUpdate.BoardId = columnRequest.BoardId;
        
        await context.SaveChangesAsync();
        return new ColumnResponse
        {
            Id = columnToUpdate.Id,
            Name = columnToUpdate.Name,
            Description = columnToUpdate.Description,
            BoardId = columnToUpdate.BoardId
        };
    }

    public async Task<bool> DeleteColumnAsync(int id)
    {
        var columnToDelete = await context.Columns.FindAsync(id);
        if (columnToDelete == null)
            throw new NotFoundException($"Column with id {id} not found");
        
        var hasCards = await context.Cards.AnyAsync(c => c.ColumnId == id);
        if (hasCards)
            throw new ConflictException($"Cannot delete column with id {id} because it has associated cards");
        
        context.Columns.Remove(columnToDelete);
        await context.SaveChangesAsync();
        return true;
    }
}