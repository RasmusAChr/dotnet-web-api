using dotnet_web_api.Dtos;
using dotnet_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ColumnController(IColumnService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ColumnResponse>>> GetAllColumns()
    {
        var columns = await service.GetAllColumnsAsync();
        return Ok(columns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ColumnResponse?>> GetColumnById(int id)
    {
        var column = await service.GetColumnByIdAsync(id);
        if (column == null)
            return NotFound();
        return Ok(column);
    }

    [HttpPost]
    public async Task<ActionResult<ColumnResponse>> AddColumn(CreateColumnRequest column)
    {
        var newColumn = await service.AddColumnAsync(column);
        // if (newColumn == null)
        //     return NotFound($"Board with id {column.BoardId} does not exist.");
        return CreatedAtAction(nameof(GetColumnById), new { id = newColumn.Id }, newColumn);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ColumnResponse>> UpdateColumn(int id, UpdateColumnRequest column)
    {
        var updatedColumn = await service.UpdateColumnAsync(id, column);
        return Ok(updatedColumn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteColumn(int id)
    {
        await service.DeleteColumnAsync(id);
        return NoContent();
    }
}
