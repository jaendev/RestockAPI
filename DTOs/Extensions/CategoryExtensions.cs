// DTOs/Extensions/CategoryExtensions.cs
using RestockAPI.Models;

namespace RestockAPI.DTOs.Extensions;

public static class CategoryExtensions
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Color = category.Color,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt,
            
            // Product statistics
            TotalProducts = category.Products?.Count ?? 0,
            ActiveProducts = category.Products?.Count(p => p.IsActive) ?? 0,
            InactiveProducts = category.Products?.Count(p => !p.IsActive) ?? 0,
            LowStockProducts = category.Products?.Count(p => p.IsActive && p.IsLowStock) ?? 0,
            OutOfStockProducts = category.Products?.Count(p => p.IsActive && p.IsOutOfStock) ?? 0,
            
            // Additional calculations
            TotalCategoryValue = category.Products?.Where(p => p.IsActive)
                                                  .Sum(p => p.TotalValue) ?? 0,
            DaysOld = (DateTime.UtcNow - category.CreatedAt).Days
        };
    }

    public static CategorySummaryDto ToSummaryDto(this Category category)
    {
        var activeProducts = category.Products?.Where(p => p.IsActive).ToList() ?? new List<Product>();
        var lowStockCount = activeProducts.Count(p => p.IsLowStock);
        
        return new CategorySummaryDto
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            IsActive = category.IsActive,
            ProductCount = activeProducts.Count,
            LowStockCount = lowStockCount,
            StatusText = GetCategoryStatusText(activeProducts.Count, lowStockCount, category.IsActive)
        };
    }

    public static Category ToEntity(this CreateCategoryDto dto)
    {
        return new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            Color = dto.Color,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(this Category category, UpdateCategoryDto dto)
    {
        category.Name = dto.Name;
        category.Description = dto.Description;
        category.Color = dto.Color;
        category.IsActive = dto.IsActive;
    }

    private static string GetCategoryStatusText(int productCount, int lowStockCount, bool isActive)
    {
        if (!isActive)
            return "Inactiva";
            
        if (productCount == 0)
            return "Sin productos";
            
        if (lowStockCount > 0)
            return $"{lowStockCount} con stock bajo";
            
        return $"{productCount} productos";
    }
    
    public static CategoryStatsDto ToStatsDto(this Category category)
    {
        var products = category.Products.ToList();
        var activeProducts = products.Where(p => p.IsActive).ToList();

        // Stock stats calculated
        var lowStockProducts = activeProducts.Where(p => p.IsLowStock && !p.IsOutOfStock).ToList();
        var outOfStockProducts = activeProducts.Where(p => p.IsOutOfStock).ToList();
        var normalStockProducts = activeProducts.Where(p => !p.IsLowStock && !p.IsOutOfStock).ToList();

        // Financial stats calculated
        var totalValue = activeProducts.Sum(p => p.CurrentStock * (p.Price ?? 0));
        var productsWithPrice = activeProducts.Where(p => p.Price.HasValue && p.Price > 0).ToList();
        var averagePrice = productsWithPrice.Any() ? productsWithPrice.Average(p => p.Price!.Value) : 0;
        var totalStock = activeProducts.Sum(p => p.CurrentStock);

        // Health metrics calculated
        var healthyProducts = normalStockProducts.Count + lowStockProducts.Count;
        var stockHealthPercentage = activeProducts.Any() 
            ? (decimal)healthyProducts / activeProducts.Count * 100 
            : 0;

        // Status checks
        var hasCriticalItems = outOfStockProducts.Any();
        var requiresAttention = lowStockProducts.Any() || outOfStockProducts.Any();

        return new CategoryStatsDto
        {
            // Basic information category
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Color = category.Color,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt,

            // Products stats
            TotalProducts = products.Count,
            ActiveProducts = activeProducts.Count,
            InactiveProducts = products.Count - activeProducts.Count,

            // Stock stats
            LowStockProducts = lowStockProducts.Count,
            OutOfStockProducts = outOfStockProducts.Count,
            NormalStockProducts = normalStockProducts.Count,

            // Financial stats
            TotalValue = Math.Round(totalValue, 2),
            AverageProductPrice = Math.Round(averagePrice, 2),
            TotalStock = totalStock,

            // Calculated metrics
            StockHealthPercentage = Math.Round(stockHealthPercentage, 2),
            HasCriticalItems = hasCriticalItems,
            RequiresAttention = requiresAttention
        };
    }
}