using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllAsync();
    Task<ReservationDto?> GetByIdAsync(int idReservation);
    Task<ReservationDto?> CreateAsync(CreateReservationDto dto);
    Task<bool> DeleteAsync(int idReservation);
}
