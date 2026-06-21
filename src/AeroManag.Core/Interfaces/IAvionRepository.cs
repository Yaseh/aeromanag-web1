using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IAvionRepository
{
    Task<IEnumerable<Avion>> GetAllAsync();
    Task<Avion?> GetByIdAsync(int idAvion);
    Task<Avion> AddAsync(Avion avion);
    Task<bool> UpdateAsync(Avion avion);
    Task<bool> DeleteAsync(int idAvion);
}
