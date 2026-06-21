namespace AeroManag.Core.DTOs;

public class ReservationDto
{
    public int IdReservation { get; set; }
    public string NumeroSiege { get; set; } = string.Empty;
    public int IdVol { get; set; }
    public string NumeroVol { get; set; } = string.Empty;
    public int IdPassager { get; set; }
    public string PassagerNom { get; set; } = string.Empty;
    public string PassagerPrenom { get; set; } = string.Empty;
}

public class CreateReservationDto
{
    public string NumeroSiege { get; set; } = string.Empty;
    public int IdVol { get; set; }
    public int IdPassager { get; set; }
}
