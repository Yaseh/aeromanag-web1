namespace AeroManag.Core.Exceptions;

public class SiegeIndisponibleException : Exception
{
    public SiegeIndisponibleException(string numeroSiege, int idVol)
        : base($"Le siege {numeroSiege} est deja reserve sur le vol {idVol}.")
    {
    }
}
