using RestockAPI.DTOs;

namespace RestockAPI.Services.Interfaces;

public interface IInventoryAlertService
{
    // ================================
    // GET METHODS - Return DTOs for display  
    // ================================
    Task<IEnumerable<InventoryAlertDto>> GetAllAlertsAsync();
    Task<IEnumerable<InventoryAlertDto>> GetActiveAlertsAsync();
    Task<IEnumerable<InventoryAlertDto>> GetAcknowledgedAlertsAsync();
    Task<IEnumerable<InventoryAlertDto>> GetResolvedAlertsAsync();
    Task<InventoryAlertDto?> GetAlertByIdAsync(int id);
    
    // ================================
    // FILTERS AND SEARCH
    // ================================
    Task<IEnumerable<InventoryAlertDto>> GetAlertsByProductIdAsync(int productId);
    Task<IEnumerable<InventoryAlertDto>> GetAlertsByTypeAsync(int alertTypeId);
    Task<IEnumerable<InventoryAlertDto>> GetAlertsByStatusAsync(int alertStatusId);
    Task<IEnumerable<InventoryAlertDto>> GetAlertsByCategoryAsync(int categoryId);
    Task<IEnumerable<InventoryAlertDto>> GetRecentAlertsAsync(int days = 7);
    
    // ================================
    // PRIORITY AND URGENCY
    // ================================
    Task<IEnumerable<InventoryAlertDto>> GetHighPriorityAlertsAsync();
    Task<IEnumerable<InventoryAlertDto>> GetCriticalAlertsAsync(); // Out of stock alerts
    Task<IEnumerable<InventoryAlertDto>> GetOldUnresolvedAlertsAsync(int daysOld = 7);
    
    // ================================
    // CRUD OPERATIONS
    // ================================
    Task<InventoryAlertDto> CreateAlertAsync(CreateInventoryAlertDto dto);
    Task<InventoryAlertDto?> UpdateAlertAsync(int id, UpdateInventoryAlertDto dto);
    Task<bool> DeleteAlertAsync(int id);
    
    // ================================
    // STATUS MANAGEMENT
    // ================================
    Task<bool> MarkAsAcknowledgedAsync(int alertId);
    Task<bool> MarkAsResolvedAsync(int alertId);
    Task<bool> MarkAsActiveAsync(int alertId); // Reopen alert
    // Task<int> BulkAcknowledgeAlertsAsync(IEnumerable<int> alertIds);
    // Task<int> BulkResolveAlertsAsync(IEnumerable<int> alertIds);
    
    // ================================
    // STATISTICS AND ANALYTICS
    // ================================
    Task<AlertStatsDto> GetAlertStatsAsync();
    Task<IEnumerable<AlertsByTypeDto>> GetAlertsByTypeStatsAsync();
    // Task<IEnumerable<AlertsByProductDto>> GetMostAlertedProductsAsync(int topCount = 10);
    
    // ================================
    // VALIDATION AND UTILITIES
    // ================================
    // Task<bool> AlertExistsAsync(int id);
    // Task<bool> HasActiveAlertsAsync(int productId);
    // Task<int> GetActiveAlertCountAsync();
    
    // ================================
    // AUTOMATIC ALERT GENERATION
    // ================================
    Task<int> GenerateStockAlertsAsync(); // Check all products and create alerts
    Task<bool> CreateLowStockAlertAsync(int productId);
    Task<bool> CreateOutOfStockAlertAsync(int productId);
}