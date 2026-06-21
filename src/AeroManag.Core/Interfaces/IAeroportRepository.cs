using AeroManag.Core.Entities;

namespace AeroManag.Core.Interfaces;

public interface IAeroportRepository
{
    Task<IEnumerable<Aeroport>> GetAllAsync();
    Task<Aeroport?> GetByIdAsync(string idIata);
    Task<Aeroport> AddAsync(Aeroport aeroport);
    Task<bool> UpdateAsync(Aeroport aeroport);
    Task<bool> DeleteAsync(string idIata);
}
