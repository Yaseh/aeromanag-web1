using AeroManag.Core.DTOs;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolsController : ControllerBase
{
    private readonly IVolService _service;

    public VolsController(IVolService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vols = await _service.GetAllAsync();
        return Ok(vols);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vol = await _service.GetByIdAsync(id);
        if (vol is null) return NotFound();
        return Ok(vol);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVolDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created!.IdVol }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreateVolDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
