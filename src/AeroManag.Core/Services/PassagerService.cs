using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class PassagerService : IPassagerService
{
    private readonly IPassagerRepository _repository;

    public PassagerService(IPassagerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PassagerDto>> GetAllAsync()
    {
        var passagers = await _repository.GetAllAsync();
        return passagers.Select(p => new PassagerDto
        {
            IdPassager = p.IdPassager,
            Nom = p.Nom,
            Prenom = p.Prenom,
            Nationalite = p.Nationalite
        });
    }

    public async Task<PassagerDto?> GetByIdAsync(int idPassager)
    {
        var p = await _repository.GetByIdAsync(idPassager);
        if (p is null) return null;
        return new PassagerDto { IdPassager = p.IdPassager, Nom = p.Nom, Prenom = p.Prenom, Nationalite = p.Nationalite };
    }

    public async Task<PassagerDto> CreateAsync(CreatePassagerDto dto)
    {
        var passager = new Passager { Nom = dto.Nom, Prenom = dto.Prenom, Nationalite = dto.Nationalite };
        var created = await _repository.AddAsync(passager);
        return new PassagerDto { IdPassager = created.IdPassager, Nom = created.Nom, Prenom = created.Prenom, Nationalite = created.Nationalite };
    }

    public async Task<bool> UpdateAsync(int idPassager, CreatePassagerDto dto)
    {
        var passager = new Passager { IdPassager = idPassager, Nom = dto.Nom, Prenom = dto.Prenom, Nationalite = dto.Nationalite };
        return await _repository.UpdateAsync(passager);
    }

    public async Task<bool> DeleteAsync(int idPassager)
    {
        return await _repository.DeleteAsync(idPassager);
    }
}
