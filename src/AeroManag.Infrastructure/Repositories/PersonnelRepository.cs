using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class PersonnelRepository : IPersonnelRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public PersonnelRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Personnel>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Personnel>(
            "SELECT id_personnel AS IdPersonnel, nom AS Nom, prenom AS Prenom, role AS Role FROM personnels");
    }

    public async Task<Personnel?> GetByIdAsync(int idPersonnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Personnel>(
            "SELECT id_personnel AS IdPersonnel, nom AS Nom, prenom AS Prenom, role AS Role FROM personnels WHERE id_personnel = @IdPersonnel",
            new { IdPersonnel = idPersonnel });
    }

    public async Task<Personnel> AddAsync(Personnel personnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newId = await connection.ExecuteScalarAsync<long>(
            "INSERT INTO personnels (nom, prenom, role) VALUES (@Nom, @Prenom, @Role); SELECT last_insert_rowid();",
            personnel);
        personnel.IdPersonnel = (int)newId;
        return personnel;
    }

    public async Task<bool> UpdateAsync(Personnel personnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE personnels SET nom = @Nom, prenom = @Prenom, role = @Role WHERE id_personnel = @IdPersonnel",
            personnel);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idPersonnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM personnels WHERE id_personnel = @IdPersonnel",
            new { IdPersonnel = idPersonnel });
        return rows > 0;
    }
}
