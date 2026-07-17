using dotnet_web_api.Dtos;
using dotnet_web_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardController(IBoardService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BoardResponse>>> GetBoards()
    {
        return Ok(await service.GetAllBoardsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardResponse>> GetBoard(int id)
    {
        var board = await service.GetBoardByIdAsync(id);
        if (board == null)
            return NotFound("Board with given id was not found.");
        return Ok(board);
    }

    [HttpPost]
    public async Task<ActionResult<BoardResponse>> CreateBoard(CreateBoardRequest board)
    {
        var createdBoard = await service.AddBoardAsync(board);
        return CreatedAtAction(nameof(GetBoard), new { id = createdBoard.Id }, createdBoard);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BoardResponse>> UpdateBoard(int id, UpdateBoardRequest board)
    {
        var updatedBoard = await service.UpdateBoardAsync(id, board);
        return Ok(updatedBoard);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBoard(int id)
    {
        await service.DeleteBoardAsync(id);
        return NoContent();
    }
}