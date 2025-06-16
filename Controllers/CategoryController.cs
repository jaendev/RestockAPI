using Microsoft.AspNetCore.Mvc;
using RestockAPI.DTOs;
using RestockAPI.Services.Interfaces;

[ApiController]
[Route("api/category/")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    // ================================
    // GET ENDPOINTS
    // ================================

    [HttpGet("getCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("getActiveCategories")]
    public async Task<IActionResult> GetActiveCategories()
    {
        var categories = await _categoryService.GetActiveCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("getCategoryById/{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        if(!await _categoryService.CategoryExistsAsync(id))
        {
            return NotFound($"Categoría con ID {id} no encontrada");
        }
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpGet("getCategoryWithStats/{id:int}")]
    public async Task<IActionResult> GetCategoryWithStats(int id)
    {
        var categoryStats = await _categoryService.GetCategoryWithStatsAsync(id);
    
        if (categoryStats == null)
            return NotFound($"Categoría con ID {id} no encontrada");
    
        return Ok(categoryStats);
    }
    
    // ================================
    // CRUD ENDPOINTS  
    // ================================

    [HttpPost("createCategory")]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        if(!await _categoryService.IsCategoryNameUniqueAsync(dto.Name))
        {
            return BadRequest("Ya existe una categoría con ese nombre");
        }
    
        var category = await _categoryService.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
    }

    [HttpPatch("updateCategory/{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
    {
        if(!await _categoryService.CategoryExistsAsync(id))
        {
            return NotFound($"Categoría con ID {id} no encontrada");
        }
    
        var category = await _categoryService.UpdateCategoryAsync(id, dto);
        return Ok(category);
    }

    [HttpDelete("deleteCategory/{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (!await _categoryService.CategoryExistsAsync(id))
        {
            return NotFound($"Categoría con ID {id} no encontrada");
        }
        
        var category = await _categoryService.DeleteCategoryAsync(id);
        return Ok(category);
    }

    [HttpPatch("toggleCategoryActive/{id:int}")]
    public async Task<IActionResult> ToggleCategoryActive(int id)
    {
        var updated = await _categoryService.ToggleCategoryActiveAsync(id);
        if(!updated) return NotFound();
        
        return NoContent();
    }
    
    // ================================
    // ANALYTICS ENDPOINTS
    // ================================

    [HttpGet("getCategoriesWithLowStock")]
    public async Task<IActionResult> GetCategoriesWithLowStock()
    {
        var categories = await _categoryService.GetCategoriesWithLowStockProductsAsync();
        return Ok(categories);
    }

    [HttpGet("getCategoriesWithOutOfStock")]
    public async Task<IActionResult> GetCategoriesWithOutOfStock()
    {
        var categories = await _categoryService.GetCategoriesWithOutOfStockProductsAsync();
        return Ok(categories);
    }

    [HttpGet("getCategoriesByProductCount")]
    public async Task<IActionResult> GetCategoriesByProductCount()
    {
        var categories = await _categoryService.GetCategoriesOrderedByProductCountAsync();
        return Ok(categories);
    }

    [HttpGet("getCategoriesByValue")]
    public async Task<IActionResult> GetCategoriesByValue()
    {
        var categories = await _categoryService.GetCategoriesOrderedByValueAsync();
        return Ok(categories);
    }

    [HttpGet("getMostValuableCategory")]
    public async Task<IActionResult> GetMostValuableCategory()
    {
        var category = await _categoryService.GetMostValuableCategoryAsync();
        return Ok(category);
    }
    
    [HttpGet("getLeastValuableCategory")]
    public async Task<IActionResult> GetLeastValuableCategory()
    {
        var category = await _categoryService.GetLeastValuableCategoryAsync();
        return Ok(category);
    }

    [HttpGet("getEmptyCategories")]
    public async Task<IActionResult> GetEmptyCategories()
    {
        var categories = await _categoryService.GetEmptyCategoriesAsync();
        return Ok(categories);
    }
    
    // ================================
    // SEARCH ENDPOINTS
    // ================================

    [HttpGet("searchCategories/{query}")]
    public async Task<IActionResult> SearchCategories(string query)
    {
        var categories = await _categoryService.SearchCategoriesAsync(query);
        return Ok(categories);
    }

    [HttpGet("getCategoriesByColor/{color:string}")]
    public async Task<IActionResult> GetCategoriesByColor(string color)
    {
        var categories = await _categoryService.GetCategoriesByColorAsync(color);
        return Ok(categories);
    }
    
    // ================================
    // UTILITY ENDPOINTS
    // ================================

    [HttpGet("getTotalProductCount/{id:int}")]
    public async Task<IActionResult> GetTotalProductCount(int id)
    {
        var count = await _categoryService.GetTotalProductCountByCategoryAsync(id);
        return Ok(count);
    }

    [HttpGet("getTotalValue/{id:int}")]
    public async Task<IActionResult> GetTotalValue(int id)
    {
        var category = await _categoryService.GetTotalValueByCategoryAsync(id);
        return Ok(category);
    }
    
    // [HttpPatch("updateCategoryColors")]
    // public async Task<IActionResult> UpdateCategoryColors(Dictionary<int, string> categoryColors)
}