using Microsoft.AspNetCore.Mvc;
using RestockAPI.DTOs.UnitType;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Controllers;

[ApiController]
[Route("api/unitTypes/")]
public class UnitTypeController : ControllerBase
{
    private readonly ILogger<UnitTypeController> _logger;
    private readonly IUnitTypeService _unitTypeService;
    
    public UnitTypeController(ILogger<UnitTypeController> logger, IUnitTypeService unitTypeService)
    {
        _logger = logger;
        _unitTypeService = unitTypeService;
    }

    [HttpGet("getUnitTypes")]
    public async Task<IActionResult> GetAllUnitTypes()
    {
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();
        return Ok(unitTypes);
    }

    [HttpGet("getActiveUnitTypes")]
    public async Task<IActionResult> GetActiveUnitTypes()
    {
        var unitTypes = await _unitTypeService.GetActiveUnitTypesAsync();    
        return Ok(unitTypes);
    }

    [HttpGet("getUnitTypeById/{id:int}")]
    public async Task<IActionResult> GetUnitTypeById(int id)
    {
        var unitType = await _unitTypeService.GetUnitTypeByIdAsync(id);
        return Ok(unitType);
    }

    [HttpGet("getUnitTypeSummaries")]
    public async Task<IActionResult> GetUnitTypeSummaries() // For dropdowns
    {
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();
        return Ok(unitTypes);
    }

    [HttpGet("getActiveUnitTypeSummaries")]
    public async Task<IActionResult> GetActiveUnitTypeSummaries() // For active dropdowns
    {
        var unitTypes = await _unitTypeService.GetActiveUnitTypesAsync();
        return Ok(unitTypes);
    }


    [HttpPost("createUnitType")]
    public async Task<ActionResult<UnitTypeDto>> CreateUnitType(CreateUnitTypeDto dto)
    {
        var unitType = await _unitTypeService.CreateUnitTypeAsync(dto);
        return CreatedAtAction(nameof(GetUnitTypeById), new { id = unitType.Id }, unitType);
    }

    [HttpPatch("updateUnitType/{id:int}")]
    public async Task<IActionResult> UpdateUnitType(int id, UpdateUnitTypeDto dto)
    {
        var unitType = await _unitTypeService.UpdateUnitTypeAsync(id, dto);
        return Ok(unitType);
    }

    [HttpDelete("deleteUnitType/{id:int}")]
    public async Task<IActionResult> DeleteUnitType(int id)
    {
        await _unitTypeService.DeleteUnitTypeAsync(id);
        return NoContent();
    }

    [HttpPatch("toggleUnitTypeActive/{id:int}")]
    public async Task<IActionResult> ToggleUnitTypeActive(int id)
    {
        await _unitTypeService.ToggleUnitTypeActiveAsync(id);
        return NoContent();
    }


    [HttpGet("getProducts/{unitTypeId:int}")]
    public async Task<IActionResult> GetProductsByUnitType(int unitTypeId)
    {
        var products = await _unitTypeService.GetProductsByUnitTypeAsync(unitTypeId);
        return Ok(products);
    }

    [HttpGet("getActiveProductCount/{unitTypeId:int}")]
    public async Task<IActionResult> GetActiveProductCount(int unitTypeId)
    {
        var count = await _unitTypeService.GetActiveProductCountByUnitTypeAsync(unitTypeId);
        return Ok(count);
    }

    [HttpGet("getTotalStockValue/{unitTypeId:int}")]
    public async Task<IActionResult> GetTotalStockValue(int unitTypeId)
    {
        var totalStockValue = await _unitTypeService.GetTotalStockValueByUnitTypeAsync(unitTypeId);
        return Ok(totalStockValue);
    }


    [HttpGet("searchUnitTypes/{searchTerm}")]
    public async Task<IActionResult> SearchUnitTypes(string searchTerm)
    {
        var unitTypes = await _unitTypeService.SearchUnitTypesAsync(searchTerm);    
        return Ok(unitTypes);
    }
}