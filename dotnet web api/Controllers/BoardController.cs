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
        var card = await service.GetBoardByIdAsync(id);
        if (card == null)
            return NotFound("Board with given id was not found.");
        return Ok(card);
    }

    [HttpPost]
    public async Task<ActionResult<BoardResponse>> CreateBoard(CreateBoardRequest board)
    {
        var createdCard = await service.AddBoardAsync(board);
        return CreatedAtAction(nameof(GetBoard), new { id = createdCard.Id }, createdCard);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BoardResponse>> UpdateBoard(int id, UpdateBoardRequest board)
    {
        var updatedCard = await service.UpdateBoardAsync(id, board);
        return updatedCard ? NoContent() : NotFound("Board with given id was not found.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBoard(int id)
    {
        var deletedCard = await service.DeleteBoardAsync(id);
        return deletedCard ? NoContent() : NotFound("Board with given id was not found.");
    }
}