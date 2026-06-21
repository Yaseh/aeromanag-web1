using AeroManag.Core.DTOs;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvionsController : ControllerBase
{
    private readonly IAvionService _service;

    public AvionsController(IAvionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var avions = await _service.GetAllAsync();
        return Ok(avions);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var avion = await _service.GetByIdAsync(id);
        if (avion is null) return NotFound();
        return Ok(avion);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAvionDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdAvion }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreateAvionDto dto)
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
