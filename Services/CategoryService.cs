using System.Collections;
using Microsoft.EntityFrameworkCore;
using RestockAPI.Data.Context;
using RestockAPI.DTOs;
using RestockAPI.DTOs.Extensions;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Services;

public class CategoryService : ICategoryService
{
    public readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return categories.Select(c => c.ToDto());
    }

    public async Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return categories.Select(c => c.ToDto());
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category?.ToDto();
    }

    public async Task<CategoryStatsDto?> GetCategoryWithStatsAsync(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Unit)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null)
            return null;

        var products = category.Products.ToList();
        var activeProducts = products.Where(p => p.IsActive).ToList();

        // Stats calculcated for stock
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

        // Verification of critical state
        var hasCriticalItems = outOfStockProducts.Any();
        var requiresAttention = lowStockProducts.Any() || outOfStockProducts.Any();

        return new CategoryStatsDto
        {
            // ================================
            // BASIC CATEGORY INFORMATION
            // ================================
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Color = category.Color,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt,

            // ================================
            // PRODUCTS STATS
            // ================================
            TotalProducts = products.Count,
            ActiveProducts = activeProducts.Count,
            InactiveProducts = products.Count - activeProducts.Count,

            // ================================
            // STOCK STATS
            // ================================
            LowStockProducts = lowStockProducts.Count,
            OutOfStockProducts = outOfStockProducts.Count,
            NormalStockProducts = normalStockProducts.Count,

            // ================================
            // FINANCIAL STATS
            // ================================
            TotalValue = Math.Round(totalValue, 2),
            AverageProductPrice = Math.Round(averagePrice, 2),
            TotalStock = totalStock,

            // ================================
            // CALCULATED METRICS
            // ================================
            StockHealthPercentage = Math.Round(stockHealthPercentage, 2),
            HasCriticalItems = hasCriticalItems,
            RequiresAttention = requiresAttention
        };
    }
    
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = dto.ToEntity();
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        var createdCategory = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == category.Id);
            
        return createdCategory!.ToDto();
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return null;
        
        category.UpdateFromDto(dto);
        await _context.SaveChangesAsync();
        
        var updatedCategory = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return updatedCategory?.ToDto();
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return false;
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleCategoryActiveAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return false;
        
        category.IsActive = !category.IsActive;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<CategorySummaryDto>> GetCategoriesWithLowStockProductsAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive && c.Products.Any(p => p.CurrentStock <= p.MinimumStock))
            .OrderBy(c => c.Name)
            .ToListAsync();

        return categories.Select(c => c.ToSummaryDto());
    }
    
    public async Task<IEnumerable<CategorySummaryDto>> GetCategoriesWithOutOfStockProductsAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive && c.Products.Any(p => p.IsActive && p.CurrentStock == 0))
            .OrderBy(c => c.Name)
            .ToListAsync();

        return categories.Select(c => c.ToSummaryDto());
    }

    public async Task<IEnumerable<CategoryStatsDto>> GetCategoriesOrderedByProductCountAsync()
    {
        var categoryIds = await _context.Categories
            .Where(c => c.IsActive)
            .OrderByDescending(c => c.Products.Count)
            .Select(c => c.Id)
            .ToListAsync();
    
        var categoriesStats = new List<CategoryStatsDto>();
    
        foreach (var categoryId in categoryIds)
        {
            var stats = await GetCategoryWithStatsAsync(categoryId);
            if (stats != null)
                categoriesStats.Add(stats);
        }
    
        return categoriesStats;
    }

    public async Task<IEnumerable<CategoryStatsDto>> GetCategoriesOrderedByValueAsync()
    {
        var categoryIds = await _context.Categories
            .Where(c => c.IsActive)
            .OrderByDescending(c => c.Products.Sum(p => p.CurrentStock * (p.Price ?? 0)))
            .Select(c => c.Id)
            .ToListAsync();
    
        var categoriesStats = new List<CategoryStatsDto>();
    
        foreach (var categoryId in categoryIds)
        {
            var stats = await GetCategoryWithStatsAsync(categoryId);
            if (stats != null)
                categoriesStats.Add(stats);
        }
    
        return categoriesStats;
    }

    public async Task<CategoryStatsDto?> GetMostValuableCategoryAsync()
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Unit)
            .Where(c => c.IsActive && c.Products.Any(p => p.IsActive))
            .OrderByDescending(c => c.Products
                .Where(p => p.IsActive)
                .Sum(p => p.CurrentStock * (p.Price ?? 0)))
            .FirstOrDefaultAsync();

        return category?.ToStatsDto();
    }

    public async Task<CategoryStatsDto?> GetLeastValuableCategoryAsync()
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Unit)
            .Where(c => c.IsActive && c.Products.Any(p => p.IsActive))
            .OrderBy(c => c.Products
                .Where(p => p.IsActive)
                .Sum(p => p.CurrentStock * (p.Price ?? 0)))
            .FirstOrDefaultAsync();

        return category?.ToStatsDto();
    }
    
    public async Task<IEnumerable<CategorySummaryDto>> GetEmptyCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive && !c.Products.Any(p => p.IsActive))
            .OrderBy(c => c.Name)
            .ToListAsync();
        
        return categories.Select(c => c.ToSummaryDto());
    }

    public async Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string query)
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive && (c.Name.Contains(query) || c.Description.Contains(query)))
            .OrderBy(c => c.Name)
            .ToListAsync();
        return categories.Select(c => c.ToDto());
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByColorAsync(string color)
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .Where(c => c.IsActive && c.Color == color)
            .OrderBy(c => c.Name)
            .ToListAsync();
        
        return categories.Select(c => c.ToDto());
    }

    public async Task<int> GetTotalProductCountByCategoryAsync(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
        
        return category?.Products?.Count(p => p.IsActive) ?? 0;
    }
    
    public async Task<decimal> GetTotalValueByCategoryAsync(int categoryId)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
        
        return category?.Products
            .Where(p => p.IsActive)
            .Sum(p => p.CurrentStock * (p.Price ?? 0)) ?? 0;
    }

    public async Task<bool> CategoryExistsAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        
        return category != null;
    }
    
    public async Task<bool> IsCategoryNameUniqueAsync(string name, int? id = null)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name && c.Id != id);
        return category == null;
    }
}