using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IPassagerRepository
{
    Task<IEnumerable<Passager>> GetAllAsync();
    Task<Passager?> GetByIdAsync(int idPassager);
    Task<Passager> AddAsync(Passager passager);
    Task<bool> UpdateAsync(Passager passager);
    Task<bool> DeleteAsync(int idPassager);
}
