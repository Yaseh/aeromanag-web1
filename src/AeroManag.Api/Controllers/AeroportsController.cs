using AeroManag.Core.DTOs;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeroportsController : ControllerBase
{
    private readonly IAeroportService _service;

    public AeroportsController(IAeroportService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aeroports = await _service.GetAllAsync();
        return Ok(aeroports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var aeroport = await _service.GetByIdAsync(id);
        if (aeroport is null) return NotFound();
        return Ok(aeroport);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAeroportDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdIata }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateAeroportDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
