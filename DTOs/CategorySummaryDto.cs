namespace RestockAPI.DTOs;

public class CategorySummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    // Basic stats
    public int ProductCount { get; set; }
    public int LowStockCount { get; set; }
    public string StatusText { get; set; } = string.Empty;
}