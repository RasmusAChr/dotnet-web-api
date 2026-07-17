using dotnet_web_api.Dtos;
using dotnet_web_api.Models;
using dotnet_web_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardController(ICardService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CardResponse>>> GetCards()
    {
        return Ok(await service.GetAllCardsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CardResponse>> GetCard(int id)
    {
        var card = await service.GetCardByIdAsync(id);
        if (card == null)
            return NotFound("Card with given id was not found.");
        return Ok(card);
    }

    [HttpPost]
    public async Task<ActionResult<CardResponse>> AddCardAsync(CreateCardRequest card)
    {
        var createdCard = await service.AddCardAsync(card);
        // if (createdCard == null)
        //     return NotFound($"Column with id {card.ColumnId} was not found.");
                
        return CreatedAtAction(nameof(GetCard), new { id = createdCard.Id }, createdCard);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CardResponse>> UpdateCardAsync(int id, UpdateCardRequest card)
    {
        var updatedCard = await service.UpdateCardAsync(id, card);
        return Ok(updatedCard);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCardAsync(int id)
    {
        await service.DeleteCardAsync(id);
        return NoContent();
    }
}

