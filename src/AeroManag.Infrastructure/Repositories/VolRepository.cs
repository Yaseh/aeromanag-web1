using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class VolRepository : IVolRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    private const string SelectWithJoins = @"
        SELECT
            v.id_vol AS IdVol,
            v.numero_vol AS NumeroVol,
            v.date_depart AS DateDepart,
            v.date_arrivee AS DateArrivee,
            v.statut AS Statut,
            ad.id_iata AS AeroportDepartCode,
            ad.nom AS AeroportDepartNom,
            aa.id_iata AS AeroportArriveeCode,
            aa.nom AS AeroportArriveeNom,
            av.id_avion AS AvionId,
            av.modele AS AvionModele,
            p.id_personnel AS CommandantId,
            p.nom AS CommandantNom,
            p.prenom AS CommandantPrenom
        FROM vols v
        JOIN aeroports ad ON v.aeroport_depart = ad.id_iata
        JOIN aeroports aa ON v.aeroport_arrivee = aa.id_iata
        JOIN avions av ON v.id_avion = av.id_avion
        JOIN personnels p ON v.id_commandant = p.id_personnel";

    public VolRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<VolDto>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<VolDto>(SelectWithJoins);
    }

    public async Task<VolDto?> GetByIdAsync(int idVol)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<VolDto>(
            SelectWithJoins + " WHERE v.id_vol = @IdVol",
            new { IdVol = idVol });
    }

    public async Task<Vol> AddAsync(Vol vol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newId = await connection.ExecuteScalarAsync<long>(
            @"INSERT INTO vols (numero_vol, date_depart, date_arrivee, statut, aeroport_depart, aeroport_arrivee, id_avion, id_commandant)
              VALUES (@NumeroVol, @DateDepart, @DateArrivee, @Statut, @AeroportDepart, @AeroportArrivee, @IdAvion, @IdCommandant);
              SELECT last_insert_rowid();",
            vol);
        vol.IdVol = (int)newId;
        return vol;
    }

    public async Task<bool> UpdateAsync(Vol vol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            @"UPDATE vols SET numero_vol = @NumeroVol, date_depart = @DateDepart, date_arrivee = @DateArrivee,
              statut = @Statut, aeroport_depart = @AeroportDepart, aeroport_arrivee = @AeroportArrivee,
              id_avion = @IdAvion, id_commandant = @IdCommandant
              WHERE id_vol = @IdVol",
            vol);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int idVol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM vols WHERE id_vol = @IdVol",
            new { IdVol = idVol });
        return rows > 0;
    }
}
