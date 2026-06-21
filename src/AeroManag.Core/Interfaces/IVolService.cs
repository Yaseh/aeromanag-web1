using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IVolService
{
    Task<IEnumerable<VolDto>> GetAllAsync();
    Task<VolDto?> GetByIdAsync(int idVol);
    Task<VolDto?> CreateAsync(CreateVolDto dto);
    Task<bool> UpdateAsync(int idVol, CreateVolDto dto);
    Task<bool> DeleteAsync(int idVol);
}
