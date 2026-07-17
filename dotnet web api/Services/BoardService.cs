using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.ExceptionHandlers;
using dotnet_web_api.Models;
using dotnet_web_api.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class BoardService(
    AppDbContext context,
    IValidator<CreateBoardRequest> createValidator,
    IValidator<UpdateBoardRequest> updateValidator) : IBoardService
{
    public async Task<List<BoardResponse>> GetAllBoardsAsync()
    {
        var boards = await context.Boards.Select(board => new BoardResponse
        {
            Id = board.Id,
            Name = board.Name,
            Description = board.Description,
            Columns = board.Columns.Select(column => new ColumnResponse
            {
                Id = column.Id,
                Name = column.Name,
                Description = column.Description,
                BoardId = column.BoardId,
                Cards = column.Cards.Select(card => new CardResponse
                {
                    Id = card.Id,
                    Name = card.Name,
                    Description = card.Description,
                    ColumnId = card.ColumnId
                }).ToList()
            }).ToList()
        }).ToListAsync();

        return boards;
    }

    public async Task<BoardResponse?> GetBoardByIdAsync(int id)
    {
        var board = await context.Boards
            .Where(board => board.Id == id)
            .Select(board => new BoardResponse
            {
                Id = board.Id,
                Name = board.Name,
                Description = board.Description,
                Columns = board.Columns.Select(column => new ColumnResponse
                {
                    Id = column.Id,
                    Name = column.Name,
                    Description = column.Description,
                    BoardId = column.BoardId,
                    Cards = column.Cards.Select(card => new CardResponse
                    {
                        Id = card.Id,
                        Name = card.Name,
                        Description = card.Description,
                        ColumnId = card.ColumnId
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return board;
    }

    public async Task<BoardResponse> AddBoardAsync(CreateBoardRequest boardRequest)
    {
        await createValidator.ValidateAndThrowAsync(boardRequest);
        
        var newBoard = new Board
        {
            Name = boardRequest.Name,
            Description = boardRequest.Description
        };
        
        context.Boards.Add(newBoard);
        await context.SaveChangesAsync();

        return new BoardResponse
        {
            Id = newBoard.Id,
            Name = newBoard.Name,
            Description = newBoard.Description
        };
    }

    public async Task<BoardResponse?> UpdateBoardAsync(int id, UpdateBoardRequest boardRequest)
    {
        await updateValidator.ValidateAndThrowAsync(boardRequest);
        
        var boardToUpdate = await context.Boards.FindAsync(id);

        if (boardToUpdate == null)
            throw new NotFoundException($"Board with id {id} not found");

        boardToUpdate.Name = boardRequest.Name;
        boardToUpdate.Description = boardRequest.Description;
        
        await context.SaveChangesAsync();
        return new BoardResponse
        {
            Id = boardToUpdate.Id,
            Name = boardToUpdate.Name,
            Description = boardToUpdate.Description
        };
    }

    public async Task<bool> DeleteBoardAsync(int id)
    {
        var boardToDelete = await context.Boards.FindAsync(id);

        if (boardToDelete == null)
            return false;
        
        context.Boards.Remove(boardToDelete);
        await context.SaveChangesAsync();
        return true;
    }
}