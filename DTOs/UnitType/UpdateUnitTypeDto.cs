using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs.UnitType;

public class UpdateUnitTypeDto
{
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string? Name { get; set; }

    [StringLength(10, ErrorMessage = "El simbolo no puede superar los 10 caracteres")]
    public string? Symbol { get; set; }

    [StringLength(200, ErrorMessage = "La descripcion no puede superar los 200 caracteres")]
    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}