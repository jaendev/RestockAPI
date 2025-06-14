using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class UpdateStockDto
{
    [Required(ErrorMessage = "El nuevo stock es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public decimal NewStock { get; set; }

    [StringLength(200, ErrorMessage = "El motivo no puede exceder 200 caracteres")]
    public string? Reason { get; set; } // "Venta", "Compra", "Ajuste", "Pérdida", etc.
}