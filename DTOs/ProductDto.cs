using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Unit information
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string UnitSymbol { get; set; } = string.Empty;
    
    // Category information
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    
    // Computed properties
    public bool IsLowStock { get; set; }
    public bool IsOutOfStock { get; set; }
    public decimal TotalValue { get; set; }
    public int DaysOld { get; set; }
    public string StockStatus { get; set; } = string.Empty;
}