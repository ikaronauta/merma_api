// Models/DTOs/PreparedProducts/MermaDto.cs

using System.ComponentModel.DataAnnotations;

namespace Merma_API.Models.DTOs.PreparedProducts;

public class MermaDto
{
    [Required]
    public string NombreTienda { get; set; } = string.Empty;

    [Required]
    public string Producto { get; set; } = string.Empty;

    [Required]
    public int CantidadICG { get; set; }

    [Required]
    public string SelectMotivo { get; set; } = string.Empty;

    public IFormFile? ImagenMerma { get; set; }

    [Required]
    public string Grabado { get; set; } = string.Empty;

    public string? DetalleProducto { get; set; } = string.Empty;
}
