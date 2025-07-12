using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs.UnitType;

public class CreateUnitTypeDto
{
    [Required(ErrorMessage = "El nombre de la unidad es requerido")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El symbol es requerido")]
    [StringLength(10, ErrorMessage = "El simbolo no puede superar los 10 caracteres")]
    public string Symbol { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}