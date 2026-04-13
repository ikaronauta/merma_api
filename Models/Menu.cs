// Models/Menu.cs

using System.ComponentModel.DataAnnotations;

namespace Merma_API.Models;

public enum Status
{
    Activo, Inactivo
}

public class Menu
{
    public int Id { get; set; }

    [Required]
    public int IdMenu { get; set; }

    [Required]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public decimal PrecioSugerido { get; set; }

    [Required]
    public string CodigoBarra { get; set; } = string.Empty;

    [Required]
    public string CodigoIntegracion { get; set; } = string.Empty;

    [Required]
    public bool VisibleOnPos { get; set; } = true;

    [Required]
    public string NombreCategoriamenu { get; set; } = string.Empty;

    public string? Descripcion { get; set; }  = string.Empty;

    [Required]
    public string NombreProd { get; set; } = string.Empty;

    [Required]
    public string CantidadProd { get; set; } = string.Empty;

    [Required]
    public Status StatusProd { get; set; } = Status.Activo;

    [Required]
    public string Unidad { get; set; } = string.Empty;

    [Required]
    public string DescripcionProd { get; set; } = string.Empty;

    [Required]
    public string CodigoBarraAgrupacion { get; set; } = string.Empty;

    [Required]
    public int IdTienda { get; set; }

    [Required]
    public string nombreTienda { get; set; } = string.Empty;

    public CoffeeStore CoffeeStore { get; set; } = null!;

}
