using AeroManag.Core.DTOs;
using AeroManag.Core.Entities;
using AeroManag.Core.Interfaces;
using AeroManag.Infrastructure.Data;
using Dapper;

namespace AeroManag.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly SqliteConnectionFactory _connectionFactory;

    private const string SelectWithJoins = @"
        SELECT
            r.id_reservation AS IdReservation,
            r.numero_siege AS NumeroSiege,
            r.id_vol AS IdVol,
            v.numero_vol AS NumeroVol,
            r.id_passager AS IdPassager,
            p.nom AS PassagerNom,
            p.prenom AS PassagerPrenom
        FROM reservations r
        JOIN vols v ON r.id_vol = v.id_vol
        JOIN passagers p ON r.id_passager = p.id_passager";

    public ReservationRepository(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<ReservationDto>(SelectWithJoins);
    }

    public async Task<ReservationDto?> GetByIdAsync(int idReservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ReservationDto>(
            SelectWithJoins + " WHERE r.id_reservation = @IdReservation",
            new { IdReservation = idReservation });
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var newId = await connection.ExecuteScalarAsync<long>(
            @"INSERT INTO reservations (numero_siege, id_vol, id_passager)
              VALUES (@NumeroSiege, @IdVol, @IdPassager);
              SELECT last_insert_rowid();",
            reservation);
        reservation.IdReservation = (int)newId;
        return reservation;
    }

    public async Task<bool> DeleteAsync(int idReservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM reservations WHERE id_reservation = @IdReservation",
            new { IdReservation = idReservation });
        return rows > 0;
    }

    public async Task<bool> SiegeDejaPrisAsync(int idVol, string numeroSiege)
    {
        using var connection = _connectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM reservations WHERE id_vol = @IdVol AND numero_siege = @NumeroSiege",
            new { IdVol = idVol, NumeroSiege = numeroSiege });
        return count > 0;
    }
}
