using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class AvionService : IAvionService
{
    private readonly IAvionRepository _repository;

    public AvionService(IAvionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AvionDto>> GetAllAsync()
    {
        var avions = await _repository.GetAllAsync();
        return avions.Select(a => new AvionDto
        {
            IdAvion = a.IdAvion,
            Modele = a.Modele,
            Capacite = a.Capacite,
            Statut = a.Statut
        });
    }

    public async Task<AvionDto?> GetByIdAsync(int idAvion)
    {
        var a = await _repository.GetByIdAsync(idAvion);
        if (a is null) return null;
        return new AvionDto { IdAvion = a.IdAvion, Modele = a.Modele, Capacite = a.Capacite, Statut = a.Statut };
    }

    public async Task<AvionDto> CreateAsync(CreateAvionDto dto)
    {
        var avion = new Avion { Modele = dto.Modele, Capacite = dto.Capacite, Statut = dto.Statut };
        var created = await _repository.AddAsync(avion);
        return new AvionDto { IdAvion = created.IdAvion, Modele = created.Modele, Capacite = created.Capacite, Statut = created.Statut };
    }

    public async Task<bool> UpdateAsync(int idAvion, CreateAvionDto dto)
    {
        var avion = new Avion { IdAvion = idAvion, Modele = dto.Modele, Capacite = dto.Capacite, Statut = dto.Statut };
        return await _repository.UpdateAsync(avion);
    }

    public async Task<bool> DeleteAsync(int idAvion)
    {
        return await _repository.DeleteAsync(idAvion);
    }
}
