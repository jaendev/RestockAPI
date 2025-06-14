using RestockAPI.Models;

namespace RestockAPI.DTOs.Extensions;

public static class ProductExtensions
{
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CurrentStock = product.CurrentStock,
            MinimumStock = product.MinimumStock,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            
            // Unit info
            UnitId = product.UnitId,
            UnitName = product.Unit?.Name ?? "",
            UnitSymbol = product.Unit?.Symbol ?? "",
            
            // Category info
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? "",
            CategoryColor = product.Category?.Color ?? "",
            
            // Computed properties
            IsLowStock = product.IsLowStock,
            IsOutOfStock = product.IsOutOfStock,
            TotalValue = product.TotalValue,
            DaysOld = product.DaysOld,
            StockStatus = product.StockStatus
        };
    }

    public static Product ToEntity(this CreateProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            CurrentStock = dto.CurrentStock,
            MinimumStock = dto.MinimumStock,
            UnitId = dto.UnitId,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(this Product product, UpdateProductDto dto)
    {
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.CurrentStock = dto.CurrentStock;
        product.MinimumStock = dto.MinimumStock;
        product.UnitId = dto.UnitId;
        product.Price = dto.Price;
        product.ImageUrl = dto.ImageUrl;
        product.CategoryId = dto.CategoryId;
        product.IsActive = dto.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
    }
}