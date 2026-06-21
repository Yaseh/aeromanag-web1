using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IAeroportService
{
    Task<IEnumerable<AeroportDto>> GetAllAsync();
    Task<AeroportDto?> GetByIdAsync(string idIata);
    Task<AeroportDto> CreateAsync(CreateAeroportDto dto);
    Task<bool> UpdateAsync(string idIata, CreateAeroportDto dto);
    Task<bool> DeleteAsync(string idIata);
}
