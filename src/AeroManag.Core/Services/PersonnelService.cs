using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;

namespace AeroManag.Core.Services;

public class PersonnelService : IPersonnelService
{
    private readonly IPersonnelRepository _repository;

    public PersonnelService(IPersonnelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PersonnelDto>> GetAllAsync()
    {
        var personnels = await _repository.GetAllAsync();
        return personnels.Select(p => new PersonnelDto
        {
            IdPersonnel = p.IdPersonnel,
            Nom = p.Nom,
            Prenom = p.Prenom,
            Role = p.Role
        });
    }

    public async Task<PersonnelDto?> GetByIdAsync(int idPersonnel)
    {
        var p = await _repository.GetByIdAsync(idPersonnel);
        if (p is null) return null;
        return new PersonnelDto { IdPersonnel = p.IdPersonnel, Nom = p.Nom, Prenom = p.Prenom, Role = p.Role };
    }

    public async Task<PersonnelDto> CreateAsync(CreatePersonnelDto dto)
    {
        var personnel = new Personnel { Nom = dto.Nom, Prenom = dto.Prenom, Role = dto.Role };
        var created = await _repository.AddAsync(personnel);
        return new PersonnelDto { IdPersonnel = created.IdPersonnel, Nom = created.Nom, Prenom = created.Prenom, Role = created.Role };
    }

    public async Task<bool> UpdateAsync(int idPersonnel, CreatePersonnelDto dto)
    {
        var personnel = new Personnel { IdPersonnel = idPersonnel, Nom = dto.Nom, Prenom = dto.Prenom, Role = dto.Role };
        return await _repository.UpdateAsync(personnel);
    }

    public async Task<bool> DeleteAsync(int idPersonnel)
    {
        return await _repository.DeleteAsync(idPersonnel);
    }
}
