// Models/DTOs/MenuDto.cs

namespace Merma_API.Models.DTOs;

public class MenuDto
{
    public int Id { get; set; }

    public int IdMenu { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string precioSugerido { get; set; } = string.Empty;

    public string CodigoBarra { get; set; } = string.Empty;

    public string CodigoIntegracion { get; set; } = string.Empty;

    public bool VisibleOnPos { get; set; } = true;

    public string NombreCategoriamenu { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public string NombreProd { get; set; } = string.Empty;

    public string CantidadProd { get; set; } = string.Empty;

    public Status StatusProd { get; set; } = Status.Activo;


    public string Unidad { get; set; } = string.Empty;

    public string DescripcionProd { get; set; } = string.Empty;

    public string CodigoBarraAgrupacion { get; set; } = string.Empty;

    public int IdTienda { get; set; }

    public string nombreTienda { get; set; } = string.Empty;
}
