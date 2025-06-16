using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El color es obligatorio")]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "El color debe ser un código hex válido (#RRGGBB)")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "El color debe tener formato hex válido (ejemplo: #FF5733)")]
    public string Color { get; set; } = "#6366f1";

    public bool IsActive { get; set; } = true;
}