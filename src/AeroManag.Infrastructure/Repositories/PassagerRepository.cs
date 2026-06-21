using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class PassagerRepository : IPassagerRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public PassagerRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Passager>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Passager>(
            "SELECT id_passager AS IdPassager, nom AS Nom, prenom AS Prenom, nationalite AS Nationalite FROM passagers");
    }

    public async Task<Passager?> GetByIdAsync(int idPassager)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Passager>(
            "SELECT id_passager AS IdPassager, nom AS Nom, prenom AS Prenom, nationalite AS Nationalite FROM passagers WHERE id_passager = @IdPassager",
            new { IdPassager = idPassager });
    }

    public async Task<Passager> AddAsync(Passager passager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newId = await connection.ExecuteScalarAsync<long>(
            "INSERT INTO passagers (nom, prenom, nationalite) VALUES (@Nom, @Prenom, @Nationalite); SELECT last_insert_rowid();",
            passager);
        passager.IdPassager = (int)newId;
        return passager;
    }

    public async Task<bool> UpdateAsync(Passager passager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE passagers SET nom = @Nom, prenom = @Prenom, nationalite = @Nationalite WHERE id_passager = @IdPassager",
            passager);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idPassager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM passagers WHERE id_passager = @IdPassager",
            new { IdPassager = idPassager });
        return rows > 0;
    }
}
