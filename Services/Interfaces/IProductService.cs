using RestockAPI.DTOs;
using RestockAPI.Models;

namespace RestockAPI.Services.Interfaces;

public interface IProductService
{
    // Get methods - return DTOs for display
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<ProductDto>> GetLowStockProductsAsync();
    Task<IEnumerable<ProductDto>> GetOutOfStockProductsAsync();
    
    // CRUD methods - return DTOs or bool for success
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<ProductDto?> UpdateProductAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> ToggleProductActiveAsync(int id);
    
    // Stock management
    Task<ProductDto?> UpdateStockAsync(int id, UpdateStockDto dto);
    Task<bool> AddStockAsync(int id, decimal quantity, string? reason = null);
    Task<bool> RemoveStockAsync(int id, decimal quantity, string? reason = null);
    
    // Search and filters
    Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<ProductDto>> GetProductsByUnitAsync(int unitId);
    
    // Validation
    Task<bool> ProductExistsAsync(int id);
    Task<bool> IsProductNameUniqueAsync(string name, int? excludeId = null);
}