using AeroManag.Core.DTOs;
using AeroManag.Core.Exceptions;
using AeroManag.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeroManag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _service.GetAllAsync();
        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _service.GetByIdAsync(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created!.IdReservation }, created);
        }
        catch (SiegeIndisponibleException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
