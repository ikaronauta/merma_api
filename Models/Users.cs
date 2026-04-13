// Models/User.cs

using System.ComponentModel.DataAnnotations;


namespace Merma_API.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Usuario { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string pass { get; set; } = string.Empty;

    [Required]
    public int Ruta { get; set; }

    [Required]
    public bool estado { get; set; }

    [Required]
    public int rol { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    public ICollection<CoffeeStore> CoffeeStores { get; set; } = new List<CoffeeStore>();
}

