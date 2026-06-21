using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class AeroportRepository : IAeroportRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    public AeroportRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Aeroport>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Aeroport>(
            "SELECT id_iata AS IdIata, nom AS Nom, ville AS Ville, pays AS Pays FROM aeroports");
    }

    public async Task<Aeroport?> GetByIdAsync(string idIata)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Aeroport>(
            "SELECT id_iata AS IdIata, nom AS Nom, ville AS Ville, pays AS Pays FROM aeroports WHERE id_iata = @IdIata",
            new { IdIata = idIata });
    }

    public async Task<Aeroport> AddAsync(Aeroport aeroport)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO aeroports (id_iata, nom, ville, pays) VALUES (@IdIata, @Nom, @Ville, @Pays)",
            aeroport);
        return aeroport;
    }

    public async Task<bool> UpdateAsync(Aeroport aeroport)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE aeroports SET nom = @Nom, ville = @Ville, pays = @Pays WHERE id_iata = @IdIata",
            aeroport);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(string idIata)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM aeroports WHERE id_iata = @IdIata",
            new { IdIata = idIata });
        return rows > 0;
    }
}
