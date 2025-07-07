using RestockAPI.DTOs;

namespace RestockAPI.Services.Interfaces;

public interface ICategoryService
{
    // ================================
    // GET METHODS - Return DTOs for display
    // ================================
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync();
    Task<object> GetCantCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryStatsDto?> GetCategoryWithStatsAsync(int id);
    
    // ================================
    // CRUD METHODS - Return DTOs or bool for success
    // ================================
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
    Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
    Task<bool> DeleteCategoryAsync(int id);
    Task<bool> ToggleCategoryActiveAsync(int id);
    
    // ================================
    // ANALYTICS AND STATISTICS
    // ================================
    Task<IEnumerable<CategorySummaryDto>> GetCategoriesWithLowStockProductsAsync();
    Task<IEnumerable<CategorySummaryDto>> GetCategoriesWithOutOfStockProductsAsync();
    Task<IEnumerable<CategoryStatsDto>> GetCategoriesOrderedByProductCountAsync();
    Task<IEnumerable<CategoryStatsDto>> GetCategoriesOrderedByValueAsync();
    Task<CategoryStatsDto?> GetMostValuableCategoryAsync();
    Task<CategoryStatsDto?> GetLeastValuableCategoryAsync();
    
    // ================================
    // SEARCH AND FILTERS
    // ================================
    Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string searchTerm);
    Task<IEnumerable<CategoryDto>> GetCategoriesByColorAsync(string color);
    Task<IEnumerable<CategorySummaryDto>> GetEmptyCategoriesAsync();
    
    // ================================
    // VALIDATION METHODS
    // ================================
    Task<bool> CategoryExistsAsync(int id);
    Task<bool> IsCategoryNameUniqueAsync(string name, int? excludeId = null);
    // Task<bool> HasProductsAsync(int id);
    // Task<bool> HasActiveProductsAsync(int id);
    
    // ================================
    // BULK OPERATIONS
    // ================================
    Task<int> GetTotalProductCountByCategoryAsync(int categoryId);
    Task<decimal> GetTotalValueByCategoryAsync(int categoryId);
}