using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IVolRepository
{
    Task<IEnumerable<VolDto>> GetAllAsync();
    Task<VolDto?> GetByIdAsync(int idVol);
    Task<Vol> AddAsync(Vol vol);
    Task<bool> UpdateAsync(Vol vol);
    Task<bool> DeleteAsync(int idVol);
}
