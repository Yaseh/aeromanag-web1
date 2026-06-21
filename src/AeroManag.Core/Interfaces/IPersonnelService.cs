using AeroManag.Core.DTOs;

namespace AeroManag.Core.Interfaces;

public interface IPersonnelService
{
    Task<IEnumerable<PersonnelDto>> GetAllAsync();
    Task<PersonnelDto?> GetByIdAsync(int idPersonnel);
    Task<PersonnelDto> CreateAsync(CreatePersonnelDto dto);
    Task<bool> UpdateAsync(int idPersonnel, CreatePersonnelDto dto);
    Task<bool> DeleteAsync(int idPersonnel);
}
