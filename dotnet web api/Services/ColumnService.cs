using dotnet_web_api.Data;
using dotnet_web_api.Dtos;
using dotnet_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Services;

public class ColumnService(AppDbContext context) : IColumnService
{
    public async Task<List<ColumnResponse>> GetAllColumnsAsync()
    {
        var columns = await context.Columns.Select(c => new ColumnResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
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
                Description = c.Description
            })
            .FirstOrDefaultAsync();
        
        return column;
    }

    public async Task<ColumnResponse> AddColumnAsync(CreateColumnRequest column)
    {
        var newColumn = new Column
        {
            Name = column.Name,
            Description = column.Description
        };

        context.Columns.Add(newColumn);
        await context.SaveChangesAsync();

        return new ColumnResponse
        {
            Id = newColumn.Id,
            Name = newColumn.Name,
            Description = newColumn.Description
        };
    }

    public async Task<bool> UpdateColumnAsync(int id, UpdateColumnRequest column)
    {
        if (column.Id != id)
            return false;
        
        var columnToUpdate = await context.Columns.FindAsync(id);

        if (columnToUpdate == null)
            return false;
        
        columnToUpdate.Name = column.Name;
        columnToUpdate.Description = column.Description;
        
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteColumnAsync(int id)
    {
        var columnToDelete = await context.Columns.FindAsync(id);

        if (columnToDelete == null)
            return false;
        
        context.Columns.Remove(columnToDelete);
        await context.SaveChangesAsync();
        return true;
    }
}