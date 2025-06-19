using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class CreateInventoryAlertDto
{
    [Required(ErrorMessage = "El producto es obligatorio")]
    public int ProductId { get; set; }
    
    [Required(ErrorMessage = "El tipo de alerta es obligatorio")]
    public int AlertTypeId { get; set; }
    
    [Required(ErrorMessage = "El mensaje es obligatorio")]
    [StringLength(500, ErrorMessage = "El mensaje no puede exceder 500 caracteres")]
    public string Message { get; set; } = string.Empty;
    
    public int AlertStatusId { get; set; } = 1; // Default to Active
}