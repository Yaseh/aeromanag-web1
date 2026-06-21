using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IReservationRepository
{
    Task<IEnumerable<ReservationDto>> GetAllAsync();
    Task<ReservationDto?> GetByIdAsync(int idReservation);
    Task<Reservation> AddAsync(Reservation reservation);
    Task<bool> DeleteAsync(int idReservation);
    Task<bool> SiegeDejaPrisAsync(int idVol, string numeroSiege);
}
