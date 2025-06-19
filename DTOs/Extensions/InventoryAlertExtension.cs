using RestockAPI.Models;

namespace RestockAPI.DTOs.Extensions;

public static class InventoryAlertExtensions
{
    /// <summary>
    /// Converts an InventoryAlert entity to InventoryAlertDto with all related information
    /// </summary>
    /// <param name="alert">The InventoryAlert entity to convert</param>
    /// <returns>InventoryAlertDto with all mapped information</returns>
    public static InventoryAlertDto ToDto(this InventoryAlert alert)
    {
        return new InventoryAlertDto
        {
            // ================================
            // BASIC ALERT INFORMATION
            // ================================
            Id = alert.Id,
            ProductId = alert.ProductId,
            AlertTypeId = alert.AlertTypeId,
            AlertStatusId = alert.AlertStatusId,
            Message = alert.Message,
            CreatedAt = alert.CreatedAt,
            AcknowledgedAt = alert.AcknowledgedAt,
            ResolvedAt = alert.ResolvedAt,

            // ================================
            // PRODUCT INFORMATION
            // ================================
            ProductName = alert.Product?.Name ?? "Product not found",
            CategoryName = alert.Product?.Category?.Name ?? "No category",
            CategoryColor = alert.Product?.Category?.Color ?? "#6b7280",

            // ================================
            // ALERT TYPE INFORMATION
            // ================================
            AlertTypeName = alert.AlertType?.Name ?? "Unknown type",
            AlertTypeColor = alert.AlertType?.Color ?? "#6b7280",
            AlertTypePriority = alert.AlertType?.Priority ?? "Normal",

            // ================================
            // ALERT STATUS INFORMATION
            // ================================
            AlertStatusName = alert.AlertStatus?.Name ?? "Unknown status",
            AlertStatusColor = alert.AlertStatus?.Color ?? "#6b7280",
            IsFinalState = alert.AlertStatus?.IsFinalState ?? false,

            // ================================
            // COMPUTED PROPERTIES
            // ================================
            IsActive = alert.IsActive,
            IsResolved = alert.IsResolved,
            IsAcknowledged = alert.IsAcknowledged,
            DaysOld = alert.DaysOld,
            HoursOld = alert.HoursOld,
            TimeAgo = alert.TimeAgo,
            Priority = alert.Priority
        };
    }

    /// <summary>
    /// Converts a collection of InventoryAlert to InventoryAlertDto
    /// </summary>
    /// <param name="alerts">Collection of InventoryAlert entities</param>
    /// <returns>IEnumerable of InventoryAlertDto</returns>
    public static IEnumerable<InventoryAlertDto> ToDto(this IEnumerable<InventoryAlert> alerts)
    {
        return alerts.Select(alert => alert.ToDto());
    }

    /// <summary>
    /// Converts InventoryAlert to a simplified DTO for listings
    /// </summary>
    /// <param name="alert">The InventoryAlert entity to convert</param>
    /// <returns>InventoryAlertSummaryDto with basic information</returns>
    public static InventoryAlertSummaryDto ToSummaryDto(this InventoryAlert alert)
    {
        return new InventoryAlertSummaryDto
        {
            Id = alert.Id,
            ProductId = alert.ProductId,
            ProductName = alert.Product?.Name ?? "Product not found",
            AlertTypeName = alert.AlertType?.Name ?? "Unknown type",
            AlertStatusName = alert.AlertStatus?.Name ?? "Unknown status",
            Priority = alert.Priority,
            CreatedAt = alert.CreatedAt,
            TimeAgo = alert.TimeAgo,
            IsActive = alert.IsActive,
            RequiresAction = alert.IsActive && !alert.IsAcknowledged
        };
    }

    /// <summary>
    /// Checks if the alert requires urgent attention
    /// </summary>
    /// <param name="alert">The InventoryAlert entity to check</param>
    /// <returns>True if requires urgent attention</returns>
    public static bool RequiresUrgentAttention(this InventoryAlert alert)
    {
        // It's urgent if:
        // 1. It's an active alert AND
        // 2. (It's high priority OR it's out of stock OR it's been unacknowledged for more than 3 days)
        return alert.IsActive && 
               (alert.Priority == "Alto" || 
                alert.AlertType?.Name == "Sin Stock" ||
                (alert.DaysOld >= 3 && !alert.IsAcknowledged));
    }

    /// <summary>
    /// Gets the appropriate CSS color to display the alert in UI
    /// </summary>
    /// <param name="alert">The InventoryAlert entity</param>
    /// <returns>Hex color code for UI</returns>
    public static string GetDisplayColor(this InventoryAlert alert)
    {
        // Prioritize the status color, then the type
        if (!string.IsNullOrEmpty(alert.AlertStatus?.Color))
            return alert.AlertStatus.Color;
        
        if (!string.IsNullOrEmpty(alert.AlertType?.Color))
            return alert.AlertType.Color;
        
        // Default color based on priority
        return alert.Priority switch
        {
            "Alto" => "#ef4444",      // Red
            "Medio" => "#f59e0b",     // Amber
            "Bajo" => "#3b82f6",      // Blue
            _ => "#6b7280"            // Gray
        };
    }

    /// <summary>
    /// Gets a readable summary of elapsed time
    /// </summary>
    /// <param name="alert">The InventoryAlert entity</param>
    /// <returns>Description of elapsed time</returns>
    public static string GetDetailedTimeAgo(this InventoryAlert alert)
    {
        var timeSpan = DateTime.UtcNow - alert.CreatedAt;
        
        return timeSpan.TotalDays switch
        {
            < 1 when timeSpan.TotalHours < 1 => $"{(int)timeSpan.TotalMinutes} minutes ago",
            < 1 when timeSpan.TotalHours < 2 => "1 hour ago",
            < 1 => $"{(int)timeSpan.TotalHours} hours ago",
            < 2 => "1 day ago",
            < 7 => $"{(int)timeSpan.TotalDays} days ago",
            < 14 => "1 week ago",
            < 30 => $"{(int)(timeSpan.TotalDays / 7)} weeks ago",
            < 60 => "1 month ago",
            _ => $"{(int)(timeSpan.TotalDays / 30)} months ago"
        };
    }

    // ============================================
    // TO ENTITY METHODS - Convert DTOs back to Entity
    // ============================================

    /// <summary>
    /// Converts CreateInventoryAlertDto to InventoryAlert entity for creating new alerts
    /// </summary>
    /// <param name="dto">DTO with data to create the alert</param>
    /// <returns>New InventoryAlert entity</returns>
    public static InventoryAlert ToEntity(this CreateInventoryAlertDto dto)
    {
        return new InventoryAlert
        {
            ProductId = dto.ProductId,
            AlertTypeId = dto.AlertTypeId,
            AlertStatusId = dto.AlertStatusId,
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow,
            AcknowledgedAt = null,
            ResolvedAt = null
            // Navigation properties will be loaded automatically by EF
            // Computed properties ([NotMapped]) are not included
        };
    }

    /// <summary>
    /// Updates an existing InventoryAlert entity with data from UpdateInventoryAlertDto
    /// Only updates fields that are not null in the DTO
    /// </summary>
    /// <param name="entity">Existing entity to update</param>
    /// <param name="dto">DTO with data to update</param>
    /// <returns>The same updated entity</returns>
    public static InventoryAlert UpdateFromDto(this InventoryAlert entity, UpdateInventoryAlertDto dto)
    {
        // Only update fields that are not null in the DTO
        if (!string.IsNullOrEmpty(dto.Message))
            entity.Message = dto.Message;

        if (dto.AlertStatusId.HasValue)
            entity.AlertStatusId = dto.AlertStatusId.Value;

        if (dto.AcknowledgedAt.HasValue)
            entity.AcknowledgedAt = dto.AcknowledgedAt.Value;

        if (dto.ResolvedAt.HasValue)
            entity.ResolvedAt = dto.ResolvedAt.Value;

        return entity;
    }

    /// <summary>
    /// Converts complete InventoryAlertDto back to entity (useful for full updates)
    /// </summary>
    /// <param name="dto">Complete DTO with all data</param>
    /// <returns>InventoryAlert entity</returns>
    public static InventoryAlert ToEntity(this InventoryAlertDto dto)
    {
        return new InventoryAlert
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            AlertTypeId = dto.AlertTypeId,
            AlertStatusId = dto.AlertStatusId,
            Message = dto.Message,
            CreatedAt = dto.CreatedAt,
            AcknowledgedAt = dto.AcknowledgedAt,
            ResolvedAt = dto.ResolvedAt
            // Navigation properties and computed properties are excluded
        };
    }

    /// <summary>
    /// Creates a new low stock alert automatically for a product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="currentStock">Current stock of the product</param>
    /// <param name="minimumStock">Minimum stock of the product</param>
    /// <returns>New InventoryAlert entity for low stock</returns>
    public static InventoryAlert CreateLowStockAlert(int productId, decimal currentStock, decimal minimumStock)
    {
        return new InventoryAlert
        {
            ProductId = productId,
            AlertTypeId = 1, // Assuming 1 = "Low Stock"
            AlertStatusId = 1, // Assuming 1 = "Active"
            Message = $"Low stock detected: Current stock ({currentStock}) below minimum ({minimumStock})",
            CreatedAt = DateTime.UtcNow,
            AcknowledgedAt = null,
            ResolvedAt = null
        };
    }

    /// <summary>
    /// Creates a new out of stock alert automatically
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <returns>New InventoryAlert entity for out of stock</returns>
    public static InventoryAlert CreateOutOfStockAlert(int productId)
    {
        return new InventoryAlert
        {
            ProductId = productId,
            AlertTypeId = 2, // Assuming 2 = "Out of Stock"
            AlertStatusId = 1, // Assuming 1 = "Active"
            Message = "Product completely out of stock - Requires immediate replenishment",
            CreatedAt = DateTime.UtcNow,
            AcknowledgedAt = null,
            ResolvedAt = null
        };
    }

    /// <summary>
    /// Marks an alert as acknowledged
    /// </summary>
    /// <param name="entity">InventoryAlert entity to mark</param>
    /// <returns>The same updated entity</returns>
    public static InventoryAlert MarkAsAcknowledged(this InventoryAlert entity)
    {
        entity.AlertStatusId = 2; // Assuming 2 = "Acknowledged"
        entity.AcknowledgedAt = DateTime.UtcNow;
        return entity;
    }

    /// <summary>
    /// Marks an alert as resolved
    /// </summary>
    /// <param name="entity">InventoryAlert entity to mark</param>
    /// <returns>The same updated entity</returns>
    public static InventoryAlert MarkAsResolved(this InventoryAlert entity)
    {
        entity.AlertStatusId = 3; // Assuming 3 = "Resolved"
        entity.ResolvedAt = DateTime.UtcNow;
        
        // If it wasn't acknowledged, also mark it as acknowledged
        if (!entity.AcknowledgedAt.HasValue)
            entity.AcknowledgedAt = DateTime.UtcNow;
            
        return entity;
    }

    /// <summary>
    /// Reactivates an alert (returns it to active state)
    /// </summary>
    /// <param name="entity">InventoryAlert entity to reactivate</param>
    /// <returns>The same updated entity</returns>
    public static InventoryAlert MarkAsActive(this InventoryAlert entity)
    {
        entity.AlertStatusId = 1; // Assuming 1 = "Active"
        entity.AcknowledgedAt = null;
        entity.ResolvedAt = null;
        return entity;
    }
}