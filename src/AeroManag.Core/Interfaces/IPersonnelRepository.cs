using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IPersonnelRepository
{
    Task<IEnumerable<Personnel>> GetAllAsync();
    Task<Personnel?> GetByIdAsync(int idPersonnel);
    Task<Personnel> AddAsync(Personnel personnel);
    Task<bool> UpdateAsync(Personnel personnel);
    Task<bool> DeleteAsync(int idPersonnel);
}
