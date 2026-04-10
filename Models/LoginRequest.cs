namespace Merma_API.Models;

public class LoginRequest
{
    public required string Usuario { get; set; }
    
    public required string Password { get; set; }
}