// Models\DTOs\PreparedProducts\CoffeeStoreDto.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merma_API.Models.DTOs;

public class CoffeeStoreDto
{
    public int Id { get; set; }

    [Required]
    public int IdTienda { get; set; } = 0;

    [Required]
    [MaxLength(100)]
    public string Tienda { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string TokenClave { get; set; } = string.Empty;

    [Required]
    public int Ruta { get; set; }

    [Required]
    [MaxLength(10)]
    public string almacen { get; set; } = string.Empty;
}