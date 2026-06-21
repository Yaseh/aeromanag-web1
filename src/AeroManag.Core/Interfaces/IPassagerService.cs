using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IPassagerService
{
    Task<IEnumerable<PassagerDto>> GetAllAsync();
    Task<PassagerDto?> GetByIdAsync(int idPassager);
    Task<PassagerDto> CreateAsync(CreatePassagerDto dto);
    Task<bool> UpdateAsync(int idPassager, CreatePassagerDto dto);
    Task<bool> DeleteAsync(int idPassager);
}
