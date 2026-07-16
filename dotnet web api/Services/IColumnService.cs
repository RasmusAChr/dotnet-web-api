using dotnet_web_api.Dtos;

namespace dotnet_web_api.Services;

public interface IColumnService
{
    Task<List<ColumnResponse>> GetAllColumnsAsync();
    Task<ColumnResponse?> GetColumnByIdAsync(int id);
    Task<ColumnResponse?> AddColumnAsync(CreateColumnRequest column);
    Task<bool> UpdateColumnAsync(int id, UpdateColumnRequest column);
    Task<bool> DeleteColumnAsync(int id);
}