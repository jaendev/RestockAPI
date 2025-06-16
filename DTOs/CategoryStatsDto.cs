namespace RestockAPI.DTOs;

public class CategoryStatsDto
{
    // ================================
    // BASIC DATA TO THE CATEGORY
    // ================================
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Color { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // ================================
    // PRODUCTS STATS
    // ================================
    public int TotalProducts { get; set; }
    public int ActiveProducts { get; set; }
    public int InactiveProducts { get; set; }

    // ================================
    // STOCK STATS
    // ================================
    public int LowStockProducts { get; set; }
    public int OutOfStockProducts { get; set; }
    public int NormalStockProducts { get; set; }

    // ================================
    // FINANCIAL STATS
    // ================================
    public decimal TotalValue { get; set; }
    public decimal AverageProductPrice { get; set; }
    public decimal TotalStock { get; set; }

    // ================================
    // CALCULATED METRICS
    // ================================
    public decimal StockHealthPercentage { get; set; }
    public bool HasCriticalItems { get; set; }
    public bool RequiresAttention { get; set; }

    // ================================
    // ADITIONAL PROPERTIES CALCULATED
    // ================================
    public string StockHealthStatus => StockHealthPercentage switch
    {
        >= 80 => "Excelente",
        >= 60 => "Bueno", 
        >= 40 => "Regular",
        >= 20 => "Malo",
        _ => "Crítico"
    };

    public string StatusColor => HasCriticalItems switch
    {
        true => "#ef4444",      // Red for critic
        false when RequiresAttention => "#f59e0b",  // Ambar for atention
        _ => "#22c55e"          // Green for normal
    };

    public int TotalAlertsExpected => LowStockProducts + OutOfStockProducts;
}