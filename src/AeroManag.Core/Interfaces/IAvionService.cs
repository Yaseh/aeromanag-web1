using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IAvionService
{
    Task<IEnumerable<AvionDto>> GetAllAsync();
    Task<AvionDto?> GetByIdAsync(int idAvion);
    Task<AvionDto> CreateAsync(CreateAvionDto dto);
    Task<bool> UpdateAsync(int idAvion, CreateAvionDto dto);
    Task<bool> DeleteAsync(int idAvion);
}
