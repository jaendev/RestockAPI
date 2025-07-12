using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestockAPI.Data.Context;
using RestockAPI.DTOs;
using RestockAPI.DTOs.Extensions;
using RestockAPI.Models;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    
    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Unit)
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync();

        return products.Select(p => p.ToDto());
    }
    
    
    public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();

        return products.Select(p => p.ToDto());
    }
    
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return product?.ToDto();
    }

    public async Task<object> GetCantProductsAsync()
    {
        var countProducts = await _context.Products
            .CountAsync();

        return new
        {
            totalProducts = countProducts,
            timestamp = DateTime.UtcNow
        };
    }
    
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p =>p.Unit)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return products.Select(p => p.ToDto());
    }

    public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .Where(p => p.IsActive && p.CurrentStock <= p.MinimumStock)
            .OrderBy(p => p.CurrentStock)
            .ToListAsync();
        
        return products.Select(p => p.ToDto());
    }
    
    public async Task<object> GetCantLowStockProductsAsync()
    {
        var countLowStockProducts = await _context.Products
            .CountAsync(p => p.IsActive && p.CurrentStock <= p.MinimumStock);
        
        return new
        {
            totalProducts = countLowStockProducts,
            timestamp = DateTime.UtcNow
        };
    }

    public async Task<IEnumerable<ProductDto>> GetOutOfStockProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .Where(p => p.IsActive && p.CurrentStock == 0)
            .ToListAsync();
        
        return products.Select(p => p.ToDto());
    }
    
    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = dto.ToEntity();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        var createdProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        return createdProduct!.ToDto();
    }

    public async Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;
        
        product.UpdateFromDto(dto);
        await _context.SaveChangesAsync();
        
        var updatedProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return updatedProduct?.ToDto();
    }
    
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;
        
        _context.Products.Remove(product);
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleProductActiveAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        
        product.IsActive = !product.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<ProductDto?> UpdateStockAsync(int id, UpdateStockDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;
        
        product.CurrentStock = dto.NewStock;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        var updatedProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return updatedProduct?.ToDto();
    }

    public async Task<bool> AddStockAsync(int id, decimal quantity, string? reason = null)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        
        product.CurrentStock += quantity;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveStockAsync(int id, decimal quantity, string? reason = null)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        
        product.CurrentStock -= quantity;
        product.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .Where(p => p.IsActive &&
                        (p.Name.Contains(searchTerm)
                         || p.Description!.Contains(searchTerm)
                         || p.Category.Name.Contains(searchTerm)
                         || p.Unit.Name.Contains(searchTerm)))
            .OrderBy(p => p.Name)
            .ToListAsync();
        
        return products.Select(p => p.ToDto());
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByUnitAsync(int unitId)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Unit)
            .Where(p => p.IsActive && p.UnitId == unitId)
            .ToListAsync();
        
        return products.Select(p => p.ToDto());
    }

    public async Task<bool> ProductExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> IsProductNameUniqueAsync(string name, int? excludeId = null)
    {
        var query = _context.Products
            .Where(p => p.Name.ToLower() == name.ToLower());

        if (excludeId != null)
        {
            query = query.Where(p => p.Id != excludeId);
        }
        
        var exist = await query.AnyAsync();
        return !exist;
    }
}