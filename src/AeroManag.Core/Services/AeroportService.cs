using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class AeroportService : IAeroportService
{
    private readonly IAeroportRepository _repository;

    public AeroportService(IAeroportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AeroportDto>> GetAllAsync()
    {
        var aeroports = await _repository.GetAllAsync();
        return aeroports.Select(a => new AeroportDto
        {
            IdIata = a.IdIata,
            Nom = a.Nom,
            Ville = a.Ville,
            Pays = a.Pays
        });
    }

    public async Task<AeroportDto?> GetByIdAsync(string idIata)
    {
        var a = await _repository.GetByIdAsync(idIata);
        if (a is null) return null;
        return new AeroportDto { IdIata = a.IdIata, Nom = a.Nom, Ville = a.Ville, Pays = a.Pays };
    }

    public async Task<AeroportDto> CreateAsync(CreateAeroportDto dto)
    {
        var aeroport = new Aeroport
        {
            IdIata = dto.IdIata,
            Nom = dto.Nom,
            Ville = dto.Ville,
            Pays = dto.Pays
        };
        var created = await _repository.AddAsync(aeroport);
        return new AeroportDto { IdIata = created.IdIata, Nom = created.Nom, Ville = created.Ville, Pays = created.Pays };
    }

    public async Task<bool> UpdateAsync(string idIata, CreateAeroportDto dto)
    {
        var aeroport = new Aeroport
        {
            IdIata = idIata,
            Nom = dto.Nom,
            Ville = dto.Ville,
            Pays = dto.Pays
        };
        return await _repository.UpdateAsync(aeroport);
    }

    public async Task<bool> DeleteAsync(string idIata)
    {
        return await _repository.DeleteAsync(idIata);
    }
}
