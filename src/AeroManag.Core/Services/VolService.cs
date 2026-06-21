using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class VolService : IVolService
{
    private readonly IVolRepository _repository;

    public VolService(IVolRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VolDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<VolDto?> GetByIdAsync(int idVol)
    {
        return await _repository.GetByIdAsync(idVol);
    }

    public async Task<VolDto?> CreateAsync(CreateVolDto dto)
    {
        var vol = new Vol
        {
            NumeroVol = dto.NumeroVol,
            DateDepart = dto.DateDepart,
            DateArrivee = dto.DateArrivee,
            Statut = dto.Statut,
            AeroportDepart = dto.AeroportDepart,
            AeroportArrivee = dto.AeroportArrivee,
            IdAvion = dto.IdAvion,
            IdCommandant = dto.IdCommandant
        };
        var created = await _repository.AddAsync(vol);
        return await _repository.GetByIdAsync(created.IdVol);
    }

    public async Task<bool> UpdateAsync(int idVol, CreateVolDto dto)
    {
        var vol = new Vol
        {
            IdVol = idVol,
            NumeroVol = dto.NumeroVol,
            DateDepart = dto.DateDepart,
            DateArrivee = dto.DateArrivee,
            Statut = dto.Statut,
            AeroportDepart = dto.AeroportDepart,
            AeroportArrivee = dto.AeroportArrivee,
            IdAvion = dto.IdAvion,
            IdCommandant = dto.IdCommandant
        };
        return await _repository.UpdateAsync(vol);
    }

    public async Task<bool> DeleteAsync(int idVol)
    {
        return await _repository.DeleteAsync(idVol);
    }
}
