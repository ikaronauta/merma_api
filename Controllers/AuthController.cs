// Controllers/AuthController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

using Merma_API.Data;
using Merma_API.Models;
using Merma_API.Models.DTOs;

namespace Merma_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("API está funcionando ✅");
    }

    public class HashRequest
    {
        public string Password { get; set; } = string.Empty;
    }

    [Authorize]
    [HttpPost("hash")]
    public IActionResult Hash([FromBody] HashRequest request)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        return Ok(new { hash });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = new ApiResponse<List<UserLoginDto>>
            {
                Status = "FAIL",
                Data = new List<UserLoginDto>(),
                Message = string.Empty,
                Error = null
            };

            // Buscar el usuario
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Usuario == request.Usuario);

            if (user == null){
                response.Message = "Usuario o contraseña incorrectos";
                return Unauthorized(response);
            }

            // Verificar contraseña
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.pass); 

            if (!isValid){
                response.Message = "Usuario o contraseña incorrectos";
                return Unauthorized(response);
            }

             // Lógica JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyString = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(keyString))
                throw new Exception("JWT Key no está configurada");

            var key = Encoding.UTF8.GetBytes(keyString);

            var timeExpireString = _config["Jwt:ExpiresInMinutes"];

            if (string.IsNullOrEmpty(timeExpireString))
                throw new Exception("Expires no está configurada");

            int timeExpire = int.Parse(timeExpireString);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("usuario", user.Usuario),
                    new Claim(ClaimTypes.Name, $"{user.Usuario} {user.Ruta}")
                }),
                Expires = DateTime.UtcNow.AddMinutes(timeExpire),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            response.Status = "OK";
            response.Data.Add(new UserLoginDto
            {
                Id = user.Id,
                Usuario = user.Usuario,
                Ruta = user.Ruta,
                estado = user.estado,
                rol = user.rol,
                Nombre = user.Nombre,
                Token = tokenString
            });
            response.Message = "Inicio de sesión exitoso";
            response.Error = null;

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<List<UserLoginDto>>
                {
                    Status = "FAIL",
                    Data = new List<UserLoginDto>(),
                    Message = "No se pudo iniciar sesión",
                    Error = ex.Message
                };
            
            return BadRequest(errorResponse);
        }
    }
}