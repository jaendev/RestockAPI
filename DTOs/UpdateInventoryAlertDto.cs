using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class UpdateInventoryAlertDto
{
    [StringLength(500, ErrorMessage = "El mensaje no puede exceder 500 caracteres")]
    public string? Message { get; set; }
    
    public int? AlertStatusId { get; set; }
    public DateTime? AcknowledgedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}