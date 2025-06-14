using Microsoft.AspNetCore.Mvc;
using RestockAPI.Data.Context;
using RestockAPI.DTOs;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Controllers;

[ApiController]
[Route("api/product/")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet("getProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("getActiveProducts")]
    public async Task<IActionResult> GetActiveProducts()
    {
        var products = await _productService.GetActiveProductsAsync();
        return Ok(products);
    }

    [HttpGet("getProductById/{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        if (!await _productService.ProductExistsAsync(id))
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }

        var product = await _productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpGet("getProductsByCategory/{categoryId:int}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpGet("getLowStockProducts")]
    public async Task<IActionResult> GetLowStockProducts()
    {
        var products = await _productService.GetLowStockProductsAsync();
        return Ok(products);
    }

    [HttpGet("getOutOfStockProducts")]
    public async Task<IActionResult> GetOutOfStockProducts()
    {
        var products = await _productService.GetOutOfStockProductsAsync();
        return Ok(products);
    }

    [HttpPost("createProduct")]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
    {
        if (!await _productService.IsProductNameUniqueAsync(dto.Name))
        {
            return BadRequest("Ya existe un producto con ese nombre");
        }

        var product = await _productService.CreateProductAsync(dto);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPatch("updateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
    {
        if (!await _productService.ProductExistsAsync(id))
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }

        if (!await _productService.IsProductNameUniqueAsync(dto.Name, id))
        {
            return BadRequest("Ya existe otro producto con ese nombre");
        }

        var product = await _productService.UpdateProductAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("deleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (!await _productService.ProductExistsAsync(id))
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }

        await _productService.DeleteProductAsync(id);
        return NoContent();
    }

    [HttpPatch("toggleProductActive/{id}")]
    public async Task<IActionResult> ToggleProductActive(int id)
    {
        var updated = await _productService.ToggleProductActiveAsync(id);
        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpPatch("updateStock/{id}")]
    public async Task<IActionResult> UpdateStock(int id, UpdateStockDto dto)
    {
        if (!await _productService.ProductExistsAsync(id))
        {
            return NotFound($"Producto con ID {id} no encontrado");
        }

        var product = await _productService.UpdateStockAsync(id, dto);
        return Ok(product);
    }

    [HttpPatch("addStock/{id}")]
    public async Task<IActionResult> AddStock(int id, decimal quantity, string? reason = null)
    {
        var added = await _productService.AddStockAsync(id, quantity, reason);
        if (!added) return NotFound();

        return NoContent();
    }

    [HttpPatch("removeStock/{id}")]
    public async Task<IActionResult> RemoveStock(int id, decimal quantity, string? reason = null)
    {
        var removed = await _productService.RemoveStockAsync(id, quantity, reason);
        if (!removed) return NotFound();

        return NoContent();
    }

    [HttpGet("searchProducts/{query}")]
    public async Task<IActionResult> SearchProducts(string query)
    {
        var products = await _productService.SearchProductsAsync(query);
        return Ok(products);
    }

    [HttpGet("getProductsByUnit/{unitId:int}")]
    public async Task<IActionResult> GetProductsByUnit(int unitId)
    {
        var products = await _productService.GetProductsByUnitAsync(unitId);
        return Ok(products);
    }
}