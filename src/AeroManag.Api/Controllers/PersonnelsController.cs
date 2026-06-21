using AeroManag.Core.DTOs;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonnelsController : ControllerBase
{
    private readonly IPersonnelService _service;

    public PersonnelsController(IPersonnelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var personnels = await _service.GetAllAsync();
        return Ok(personnels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var personnel = await _service.GetByIdAsync(id);
        if (personnel is null) return NotFound();
        return Ok(personnel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonnelDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdPersonnel }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreatePersonnelDto dto)
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
