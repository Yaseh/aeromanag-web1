using AeroManag.Core.DTOs;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassagersController : ControllerBase
{
    private readonly IPassagerService _service;

    public PassagersController(IPassagerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passagers = await _service.GetAllAsync();
        return Ok(passagers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var passager = await _service.GetByIdAsync(id);
        if (passager is null) return NotFound();
        return Ok(passager);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePassagerDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdPassager }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreatePassagerDto dto)
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
