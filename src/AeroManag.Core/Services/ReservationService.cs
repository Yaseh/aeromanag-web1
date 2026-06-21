using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Exceptions;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ReservationDto?> GetByIdAsync(int idReservation)
    {
        return await _repository.GetByIdAsync(idReservation);
    }

    public async Task<ReservationDto?> CreateAsync(CreateReservationDto dto)
    {
        var siegePris = await _repository.SiegeDejaPrisAsync(dto.IdVol, dto.NumeroSiege);
        if (siegePris)
            throw new SiegeIndisponibleException(dto.NumeroSiege, dto.IdVol);

        var reservation = new Reservation
        {
            NumeroSiege = dto.NumeroSiege,
            IdVol = dto.IdVol,
            IdPassager = dto.IdPassager
        };
        var created = await _repository.AddAsync(reservation);
        return await _repository.GetByIdAsync(created.IdReservation);
    }

    public async Task<bool> DeleteAsync(int idReservation)
    {
        return await _repository.DeleteAsync(idReservation);
    }
}
