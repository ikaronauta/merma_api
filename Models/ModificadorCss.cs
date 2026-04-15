using System.ComponentModel.DataAnnotations;

namespace Merma_API.Models;

public class ModificadorCss
{
    public int Id { get; set; }

    public string codigoModif { get; set; } = string.Empty;

    public string idmodificador { get; set; } = string.Empty;

    public string nombreModif { get; set; } = string.Empty;

    public string idTipoModificador { get; set; } = string.Empty;

    public string nombreTipoModificador { get; set; } = string.Empty;

    [Required]
    public int IdTienda { get; set; }

    public string nombreTienda { get; set; } = string.Empty;

    public double costo { get; set; }

    public string cantidadProd { get; set; } = string.Empty;

    public string codigo { get; set; } = string.Empty;

    public string codigoBarra { get; set; } = string.Empty;

    public string codigoProd { get; set; } = string.Empty;

    public string descripcionProd { get; set; } = string.Empty;

    [Required]
    public int idAgrupacion { get; set; }

    public string nombre { get; set; } = string.Empty;

    public string nombreProd { get; set; } = string.Empty;

    public string status_prod { get; set; } = string.Empty;

    public string unidad { get; set; } = string.Empty;
}
