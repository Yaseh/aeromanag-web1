using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class AvionRepository : IAvionRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public AvionRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Avion>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Avion>(
            "SELECT id_avion AS IdAvion, modele AS Modele, capacite AS Capacite, statut AS Statut FROM avions");
    }

    public async Task<Avion?> GetByIdAsync(int idAvion)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Avion>(
            "SELECT id_avion AS IdAvion, modele AS Modele, capacite AS Capacite, statut AS Statut FROM avions WHERE id_avion = @IdAvion",
            new { IdAvion = idAvion });
    }

    public async Task<Avion> AddAsync(Avion avion)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newId = await connection.ExecuteScalarAsync<long>(
            "INSERT INTO avions (modele, capacite, statut) VALUES (@Modele, @Capacite, @Statut); SELECT last_insert_rowid();",
            avion);
        avion.IdAvion = (int)newId;
        return avion;
    }

    public async Task<bool> UpdateAsync(Avion avion)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE avions SET modele = @Modele, capacite = @Capacite, statut = @Statut WHERE id_avion = @IdAvion",
            avion);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idAvion)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM avions WHERE id_avion = @IdAvion",
            new { IdAvion = idAvion });
        return rows > 0;
    }
}
