using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.Models;
using dotnet_web_api.Validators;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class BoardService(AppDbContext context) : IBoardService
{
    public async Task<List<BoardResponse>> GetAllBoardsAsync()
    {
        var boards = await context.Boards.Select(board => new BoardResponse
        {
            Id = board.Id,
            Name = board.Name,
            Description = board.Description
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
                Description = board.Description
            })
            .FirstOrDefaultAsync();

        return board;
    }

    public async Task<BoardResponse> AddBoardAsync(CreateBoardRequest boardRequest)
    {
        //BoardCreateValidator validator = new BoardCreateValidator();
        //ValidationResult result = await validator.ValidateAsync(boardRequest);
        
        
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

    public async Task<bool> UpdateBoardAsync(int id, UpdateBoardRequest board)
    {
        if (board.Id != id)
            return false;
        
        var boardToUpdate = await context.Boards.FindAsync(id);

        if (boardToUpdate == null)
            return false;
        
        boardToUpdate.Name = board.Name;
        boardToUpdate.Description = board.Description;
        
        await context.SaveChangesAsync();
        return true;
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