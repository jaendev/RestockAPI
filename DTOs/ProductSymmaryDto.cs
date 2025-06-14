namespace RestockAPI.DTOs;

public class ProductSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal? Price { get; set; }
    public bool IsActive { get; set; }
    
    // Unit info
    public string UnitSymbol { get; set; } = string.Empty;
    
    // Category info
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    
    // Status
    public bool IsLowStock { get; set; }
    public bool IsOutOfStock { get; set; }
    public string StockStatus { get; set; } = string.Empty;
}