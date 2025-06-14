using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class UpdateProductDto
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El stock actual es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El stock actual debe ser mayor o igual a 0")]
    public decimal CurrentStock { get; set; }

    [Required(ErrorMessage = "El stock mínimo es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El stock mínimo debe ser mayor o igual a 0")]
    public decimal MinimumStock { get; set; }

    [Required(ErrorMessage = "El tipo de unidad es obligatorio")]
    public int UnitId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
    public decimal? Price { get; set; }

    [StringLength(500, ErrorMessage = "La URL de imagen no puede exceder 500 caracteres")]
    [Url(ErrorMessage = "La URL de imagen no tiene un formato válido")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public int CategoryId { get; set; }

    public bool IsActive { get; set; } = true;
}