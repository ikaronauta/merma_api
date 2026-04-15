// Controllers/PreparedProductsController.cs

using Merma_API.Data;
using Merma_API.Models;
using Merma_API.Models.DTOs;
using Merma_API.Models.DTOs.PreparedProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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


    // GET: /PreparedProducts/modificadores-productos
    [Authorize]
    [HttpGet("modificadores-productos")]
    public async Task<ActionResult<ApiResponse<List<object>>>> GetModificadores([FromQuery] string Nombre, [FromQuery] int IdMenu, [FromQuery] int IdTienda)
    {
        var response = new ApiResponse<List<object>>
        {
            Status = "OK",
            Data = new List<object>(),
            Message = "Modificadores/Productos consultados con éxito",
            Error = null
        };

        try
        {

            var arrModificadores = await _context.ArregloModificadors
                .Where(x =>
                    x.Nombre == Nombre &&
                    x.IdMenu == IdMenu &&
                    x.IdTienda == IdTienda)
                .Select(x => new
                {
                    x.Nombre,
                    x.modificador
                })
                .Distinct()
                .ToListAsync();

            var arrResponse = new List<object>();

            if (arrModificadores.Count > 0)
            {

                foreach (var item in arrModificadores)
                {
                    var idModificador = item.modificador;

                    var result = await _context.ModificadorCss
                    .Where(mc =>
                        mc.idmodificador == idModificador &&
                        mc.IdTienda == IdTienda)
                    .Select(mc => new
                    {
                        value = mc.idmodificador + "," +
                                mc.nombre + "," +
                                mc.codigo + "," +
                                mc.codigoBarra + "," +
                                mc.idAgrupacion,
                        text = mc.nombre
                    })
                    .ToListAsync();

                    arrResponse.Add(result);
                }

                response.Data.Add(new
                {
                    type = "modificadores",
                    data = new
                    {
                        select1 = arrResponse.Count > 0 ? arrResponse[0] : new List<object>(),
                        select2 = arrResponse.Count > 1 ? arrResponse[1] : new List<object>()
                    }
                });

                return Ok(response);
            }

            arrResponse.Add(new { value = "Vencimiento", text = "Vencimiento" });
            arrResponse.Add(new { value = "Vencimiento", text = "Vencimiento" });
            arrResponse.Add(new { value = "Empaque Deteriorado", text = "Empaque Deteriorado" });
            arrResponse.Add(new { value = "Contam.com comestib", text = "Contam.com comestib" });
            arrResponse.Add(new { value = "Contam.olores fuerte", text = "Contam.olores fuerte" });
            arrResponse.Add(new { value = "Producto Quebrado", text = "Producto Quebrado" });
            arrResponse.Add(new { value = "Deterioro de Origen", text = "Deterioro de Origen" });
            arrResponse.Add(new { value = "Producto Húmedo", text = "Producto Húmedo" });
            arrResponse.Add(new { value = "Bolsa Rota/despegada", text = "Bolsa Rota/despegada" });
            arrResponse.Add(new { value = "Inform.Defect.Empaq", text = "Inform.Defect.Empaq" });
            arrResponse.Add(new { value = "Fech.venc.no legible", text = "Fech.venc.no legible" });
            arrResponse.Add(new { value = "Baja Rotación", text = "Baja Rotación" });
            arrResponse.Add(new { value = "Prod.fuera.Portafol.", text = "Prod.fuera.Portafol." });
            arrResponse.Add(new { value = "Infestacion roedores", text = "Infestacion roedores" });
            arrResponse.Add(new { value = "Berredura", text = "Berredura" });
            arrResponse.Add(new { value = "Transporte Log Prima", text = "Transporte Log Prima" });


            response.Data.Add(new
            {
                type = "producto",
                data = new
                {
                    select1 = arrResponse
                }
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = "FAIL";
            response.Message = "Error al consultar los Modificadores/Productos";
            response.Error = ex.Message;

            return BadRequest(response);
        }
    }


    // POST: /PreparedProducts/insertar-merma
    [Authorize]
    [HttpPost("insertar-merma")]
    public async Task<ActionResult<ApiResponse<List<object>>>> InsertMerma([FromForm] MermaDto dto)
    {
        var response = new ApiResponse<List<object>>
        {
            Status = "OK",
            Data = new List<object>(),
            Message = "Merma insertada con éxito",
            Error = null
        };

        try
        {
            string rutaImagen = "";
            string responseRutaImagen = "";

            if (dto.ImagenMerma == null)
            {
                response.Status = "FAIL";
                response.Message = "Imagen requerida";
                response.Error = "Bad Request";
                return BadRequest(response);
            }
                
            if (dto.ImagenMerma.Length > 5 * 1024 * 1024)
            {
                response.Status = "FAIL";
                response.Message = "La imagen supera los 5MB";
                response.Error = "Bad Request";
                return BadRequest(response);
            }

            // TODO: Implementar API de Microsoft Graph
            var extension = Path.GetExtension(dto.ImagenMerma.FileName);
            var nombreArchivo = $"merma_{Guid.NewGuid()}{extension}";
            var folder = $"uploads/mermas/{DateTime.UtcNow:yyyy/MM/dd}";
            var rutaCarpeta = Path.Combine("wwwroot", folder);

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await dto.ImagenMerma.CopyToAsync(stream);
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            rutaImagen = $"{folder}/{nombreArchivo}";
            responseRutaImagen = $"{baseUrl}/{folder}/{nombreArchivo}";

            var merma = new Merma
            {
                NombreTienda = dto.NombreTienda,
                Producto = dto.Producto,
                CantidadICG = dto.CantidadICG,
                SelectMotivo = dto.SelectMotivo,
                FechaHoraActual = DateTime.Now,
                RutaImagenMerma = rutaImagen,
                Grabado = dto.Grabado,
                DetalleProducto = dto.DetalleProducto
            };

            _context.Mermas.Add(merma);
            await _context.SaveChangesAsync();

            response.Data.Add(new
            {
                merma.Id,
                merma.NombreTienda,
                merma.Producto,
                merma.CantidadICG,
                merma.SelectMotivo,
                merma.FechaHoraActual,
                RutaImagenMerma = responseRutaImagen,
                merma.Grabado,
                merma.DetalleProducto
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = "FAIL";
            response.Message = "Error al insertar merma";
            response.Error = ex.Message;

            return BadRequest(response);
        }
    }

}