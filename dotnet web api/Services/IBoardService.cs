using dotnet_web_api.Dtos;

namespace dotnet_web_api.Services;

public interface IBoardService
{
    Task<List<BoardResponse>> GetAllBoardsAsync();
    Task<BoardResponse?> GetBoardByIdAsync(int id);
    Task<BoardResponse> AddBoardAsync(CreateBoardRequest card);
    Task<BoardResponse> UpdateBoardAsync(int id, UpdateBoardRequest card);
    Task<bool> DeleteBoardAsync(int id);
}