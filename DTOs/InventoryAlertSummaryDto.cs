namespace RestockAPI.DTOs;

public class InventoryAlertSummaryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string AlertTypeName { get; set; } = string.Empty;
    public string AlertStatusName { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string TimeAgo { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool RequiresAction { get; set; }
}