namespace RestockAPI.DTOs.UnitType;

public class UnitTypeWithStatsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Statistics
    public int ProductCount { get; set; }
    public int ActiveProductCount { get; set; }
    public decimal TotalStockValue { get; set; }
    public decimal AverageProductPrice { get; set; }
    
    // Computed properties
    public string DisplayName => $"{Name} ({Symbol})";
    public bool HasProducts => ProductCount > 0;
    public string UsageLevel => ProductCount switch
    {
        0 => "Unused",
        <= 5 => "Low Usage", 
        <= 15 => "Medium Usage",
        _ => "High Usage"
    };
}