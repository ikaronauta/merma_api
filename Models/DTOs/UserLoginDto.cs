// Models/DTOs/UserLoginDto.cs

namespace Merma_API.Models.DTOs;

public class UserLoginDto
{
    public int Id { get; set; }

    public string Usuario { get; set; } = string.Empty;

    public int Ruta { get; set; } = 0;

    public bool estado { get; set; } = false;

    public int rol { get; set; } = 0;

    public string Token { get; set; } = string.Empty;
}