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
    public async Task<ActionResult<List<Card>>> GetCards()
    {
        return Ok(await service.GetAllCardsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCardByIdAsync(int id)
    {
        var card = await service.GetCardByIdAsync(id);
        if (card == null)
        {
            return NotFound("Card with given id was not found.");
        }
        return Ok(card);
    }
}

