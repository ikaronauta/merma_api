// Models/ArregloModificador.cs

namespace Merma_API.Models;

public class ArregloModificador
{
    public int Id { get; set; }

    public string modificador { get; set; } = string.Empty;

    public int IdMenu { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public int IdTienda { get; set; }

    public string nombreTienda { get; set; } = string.Empty;
}
