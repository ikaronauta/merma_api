// Controllers/PreparedProductsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;

using Merma_API.Data;
using Merma_API.Models;
using Merma_API.Models.DTOs;

namespace Merma_API.Controllers;

[ApiController]
[Route("[controller]")]
public class PreparedProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PreparedProductsController(AppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("sin auth funciona");
    }

    // GET: /PreparedProducts?ruta=123
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CoffeeStoreDto>>>> GetCoffeeStores([FromQuery] int ruta)
    {
        try
        {
            var coffees = await _context.CoffeeStores
                .Where(c => c.Ruta == ruta)
                .ToListAsync();

            var response = new ApiResponse<List<CoffeeStoreDto>>
            {
                Status = "OK",
                Data = coffees.Select(c => new CoffeeStoreDto
                {
                    IdTienda = c.IdTienda,
                    Tienda = c.Tienda,
                    TokenClave = c.TokenClave,
                    Ruta = c.Ruta,
                    almacen = c.almacen
                }).ToList(),
                Message = "OK"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<CoffeeStoreDto>>
            {
                Status = "FAIL",
                Data = new List<CoffeeStoreDto>(),
                Message = "Error al consultar los coffeeStore",
                Error = ex.Message
            });
        }
    }

    // GET: /PreparedProducts/menus-coffee?idCoffee=1
    [Authorize]
    [HttpGet("menus-coffee")]
    public async Task<ActionResult<ApiResponse<List<MenuDto>>>> GetMenus([FromQuery] int idTienda)
    {
        try
        {
            var menus = await _context.Menus
                .Where(m => m.IdTienda == idTienda)
                .Select(m => new MenuDto
                {
                    Id = m.Id,
                    IdMenu = m.IdMenu,
                    Codigo = m.Codigo,
                    Nombre = m.Nombre,
                    CodigoBarra = m.CodigoBarra,
                    CodigoIntegracion = m.CodigoIntegracion,
                    VisibleOnPos = m.VisibleOnPos,
                    NombreCategoriamenu = m.NombreCategoriamenu,
                    Descripcion = m.Descripcion ?? "",
                    NombreProd = m.NombreProd,
                    CantidadProd = m.CantidadProd,
                    StatusProd = m.StatusProd,
                    Unidad = m.Unidad,
                    DescripcionProd = m.DescripcionProd,
                    CodigoBarraAgrupacion = m.CodigoBarraAgrupacion,
                    IdTienda = m.IdTienda,
                    nombreTienda = m.nombreTienda
                })
                .ToListAsync();

            //Prueba
            //var data = await _context.Users
            //    .Include(u => u.CoffeeStores)
            //        .ThenInclude(cs => cs.Menus)
            //    .ToListAsync();

            var response = new ApiResponse<List<MenuDto>>
            {
                Status = "OK",
                Data = menus,
                Message = "Menus consultados con éxito"
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<MenuDto>>
            {
                Status = "FAIL",
                Data = new List<MenuDto>(),
                Message = "Error al consultar los menus del coffee",
                Error = ex.Message
            });
        }
    }
}