using Microsoft.AspNetCore.Mvc;
using RestockAPI.DTOs;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Controllers;

[ApiController]
[Route("api/alert/")]
public class InventoryAlertController : ControllerBase
{
    private readonly IInventoryAlertService _alertService;

    public InventoryAlertController(IInventoryAlertService alertService)
    {
        _alertService = alertService;
    }

    [HttpGet("getActiveAlerts")]
    public async Task<IActionResult> GetAllArts()
    {
        var alerts = await _alertService.GetAllAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getAllAlerts")]
    public async Task<IActionResult> GetActiveAlerts()
    {
        var alerts = await _alertService.GetActiveAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getAcknowledgedAlerts")]
    public async Task<IActionResult> GetAcknowledgedAlerts()
    {
        var alerts = await _alertService.GetAcknowledgedAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getResolvedAlerts")]
    public async Task<IActionResult> GetResolvedAlerts()
    {
        var alerts = await _alertService.GetResolvedAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getAlertById/{id:int}")]
    public async Task<IActionResult> GetAlertById(int id)
    {
        var alert = await _alertService.GetAlertByIdAsync(id);
        return Ok(alert);
    }

    [HttpGet("getAlertsByProduct/{productId:int}")]
    public async Task<IActionResult> GetAlertsByProduct(int productId)
    {
        var alerts = await _alertService.GetAlertsByProductIdAsync(productId);
        return Ok(alerts);
    }
    
    [HttpGet("getAlertsByType/{alertTypeId:int}")]
    public async Task<IActionResult> GetAlertsByType(int alertTypeId)
    {
        var alerts = await _alertService.GetAlertsByTypeAsync(alertTypeId);
        return Ok(alerts);
    }
    
    [HttpGet("getAlertsByStatus/{alertStatusId:int}")]
    public async Task<IActionResult> GetAlertsByStatus(int alertStatusId)
    {
        var alerts = await _alertService.GetAlertsByStatusAsync(alertStatusId);
        return Ok(alerts);
    }
    
    [HttpGet("getAlertsByCategory/{categoryId:int}")]
    public async Task<IActionResult> GetAlertsByCategory(int categoryId)
    {
        var alerts = await _alertService.GetAlertsByCategoryAsync(categoryId);
        return Ok(alerts);
    }
    
    [HttpGet("getRecentAlerts/{days:int?}")]
    public async Task<IActionResult> GetRecentAlerts(int days = 7)
    {
        var alerts = await _alertService.GetRecentAlertsAsync(days);
        return Ok(alerts);
    }
    
    [HttpGet("getHighPriorityAlerts")]
    public async Task<IActionResult> GetHighPriorityAlerts()
    {
        var alerts = await _alertService.GetHighPriorityAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getCriticalAlerts")]
    public async Task<IActionResult> GetCriticalAlerts()
    {
        var alerts = await _alertService.GetCriticalAlertsAsync();
        return Ok(alerts);
    }
    
    [HttpGet("getUnresolvedAlerts")]
    public async Task<IActionResult> GetOldUnresolvedAlerts(int days = 7)
    {
        var alerts = await _alertService.GetOldUnresolvedAlertsAsync(days);
        return Ok(alerts);
    }

    [HttpPost("createAlert")]
    public async Task<IActionResult> CreateAlert(CreateInventoryAlertDto dto)
    {
        var alert = await _alertService.CreateAlertAsync(dto);
        return Ok(alert);
    }

    [HttpPatch("updateAlert/{id:int}")]
    public async Task<IActionResult> UpdateAlert(int id, UpdateInventoryAlertDto dto)
    {
        var alert = await _alertService.UpdateAlertAsync(id, dto);
        return Ok(alert);
    }
    
    [HttpDelete("deleteAlert/{id:int}")]
    public async Task<IActionResult> DeleteAlert(int id)
    {
        await _alertService.DeleteAlertAsync(id);
        return Ok();
    }
    
    [HttpPatch("markAsAcknowledged/{id:int}")]
    public async Task<IActionResult> MarkAsAcknowledged(int id)
    {
        var alert = await _alertService.MarkAsAcknowledgedAsync(id);
        return Ok(alert);
    }
        
    [HttpPatch("markAsResolved/{id:int}")]
    public async Task<IActionResult> MarkAsResolved(int id)
    {
        var alert = await _alertService.MarkAsResolvedAsync(id);
        return Ok(alert);
    }
    
    [HttpPatch("markAsActive/{id:int}")]
    public async Task<IActionResult> MarkAsActive(int id)
    {
        var alert = await _alertService.MarkAsActiveAsync(id);
        return Ok(alert);
    }
    
    [HttpGet("getAlertStats")]
    public async Task<IActionResult> GetAlertStats()
    {
        var stats = await _alertService.GetAlertStatsAsync();
        return Ok(stats);
    }
    
    [HttpGet("getAlertsByTypeStats")]
    public async Task<IActionResult> GetAlertsByTypeStats()
    {
        var stats = await _alertService.GetAlertsByTypeStatsAsync();
        return Ok(stats);
    }
    
    [HttpPost("generateStockAlerts")]
    public async Task<IActionResult> GenerateStockAlerts()
    {
        await _alertService.GenerateStockAlertsAsync();
        return Ok();
    }
    
    [HttpPost("createLowStockAlert/{productId:int}")]
    public async Task<IActionResult> CreateLowStockAlert(int productId)
    {
        await _alertService.CreateLowStockAlertAsync(productId);
        return Ok();
    }
    
    [HttpPost("createOutOfStockAlert/{productId:int}")]
    public async Task<IActionResult> CreateOutOfStockAlert(int productId)
    {
        await _alertService.CreateOutOfStockAlertAsync(productId);
        return Ok();
    }
}