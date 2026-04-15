// Models/Merma

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merma_API.Models;

public class Merma
{
    public int Id { get; set; }

    [Required]
    [Column("nombreTienda")]
    public string NombreTienda { get; set; } = string.Empty;

    [Required]
    [Column("producto")]
    public string Producto { get; set; } = string.Empty;

    [Required]
    [Column("cantidadICG")]
    public int CantidadICG { get; set; }

    [Required]
    [Column("selectMotivo")]
    public string SelectMotivo { get; set; } = string.Empty;

    [Required]
    [Column("fechaHoraActual")]
    public DateTime FechaHoraActual { get; set; }

    [Column("imagenMerma")]
    public string RutaImagenMerma { get; set; } = string.Empty;

    [Required]
    [Column("grabado")]
    public string Grabado { get; set; } = string.Empty;

    public string? DetalleProducto { get; set; } = string.Empty;
}
