using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

// ============================================
// MAIN DTO FOR DISPLAY
// ============================================
public class InventoryAlertDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    
    public int AlertTypeId { get; set; }
    public string AlertTypeName { get; set; } = string.Empty;
    public string AlertTypeColor { get; set; } = string.Empty;
    public string AlertTypePriority { get; set; } = string.Empty;
    
    public int AlertStatusId { get; set; }
    public string AlertStatusName { get; set; } = string.Empty;
    public string AlertStatusColor { get; set; } = string.Empty;
    public bool IsFinalState { get; set; }
    
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? AcknowledgedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    
    // Computed properties
    public bool IsActive { get; set; }
    public bool IsResolved { get; set; }
    public bool IsAcknowledged { get; set; }
    public int DaysOld { get; set; }
    public int HoursOld { get; set; }
    public string TimeAgo { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
}