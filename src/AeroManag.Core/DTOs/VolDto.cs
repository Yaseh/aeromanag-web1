namespace AeroManag.Core.DTOs;

public class VolDto
{
    public int IdVol { get; set; }
    public string NumeroVol { get; set; } = string.Empty;
    public string DateDepart { get; set; } = string.Empty;
    public string DateArrivee { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public string AeroportDepartCode { get; set; } = string.Empty;
    public string AeroportDepartNom { get; set; } = string.Empty;
    public string AeroportArriveeCode { get; set; } = string.Empty;
    public string AeroportArriveeNom { get; set; } = string.Empty;
    public int AvionId { get; set; }
    public string AvionModele { get; set; } = string.Empty;
    public int CommandantId { get; set; }
    public string CommandantNom { get; set; } = string.Empty;
    public string CommandantPrenom { get; set; } = string.Empty;
}

public class CreateVolDto
{
    public string NumeroVol { get; set; } = string.Empty;
    public string DateDepart { get; set; } = string.Empty;
    public string DateArrivee { get; set; } = string.Empty;
    public string Statut { get; set; } = string.Empty;
    public string AeroportDepart { get; set; } = string.Empty;
    public string AeroportArrivee { get; set; } = string.Empty;
    public int IdAvion { get; set; }
    public int IdCommandant { get; set; }
}
