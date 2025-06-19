using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestockAPI.Data.Context;
using RestockAPI.Services.Interfaces;
using RestockAPI.DTOs;
using RestockAPI.DTOs.Extensions;
using RestockAPI.Models;

namespace RestockAPI.Services;

public class InventoryAlertService : IInventoryAlertService
{
    public readonly ApplicationDbContext _context;

    public InventoryAlertService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAllAlertsAsync()
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetActiveAlertsAsync()
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertStatus.Name == "Activa") // ✅ Fixed: Use actual SQL condition
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAcknowledgedAlertsAsync()
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertStatus.Name == "Reconocida")
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetResolvedAlertsAsync()
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertStatus.Name == "Resuelta")
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<InventoryAlertDto?> GetAlertByIdAsync(int id)
    {
        var alert = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .FirstOrDefaultAsync(a => a.Id == id);

        return alert?.ToDto();
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAlertsByProductIdAsync(int productId)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.ProductId == productId)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAlertsByTypeAsync(int alertTypeId)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertTypeId == alertTypeId)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAlertsByStatusAsync(int alertStatusId)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertStatusId == alertStatusId)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetAlertsByCategoryAsync(int categoryId)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.Product.CategoryId == categoryId)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetRecentAlertsAsync(int days)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.CreatedAt > DateTime.UtcNow.AddDays(-days))
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetHighPriorityAlertsAsync()
    {
        // TODO: implement Priority table in DB
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertType.Priority == "Alto")
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetCriticalAlertsAsync()
    {
        // Get out of stock alerts (most critical)
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.AlertType.Name == "Sin Stock") // Out of stock alerts
            .Where(a => a.AlertStatus.Name == "Activa") // Only active ones
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<InventoryAlertDto>> GetOldUnresolvedAlertsAsync(int days)
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .Where(a => a.CreatedAt < DateTime.UtcNow.AddDays(-days) && a.AlertStatus.Name != "Resuelta") // ✅ Fixed
            .ToListAsync();

        return alerts.Select(a => a.ToDto());
    }

    public async Task<InventoryAlertDto> CreateAlertAsync(CreateInventoryAlertDto dto)
    {
        var alert = dto.ToEntity();
        _context.InventoryAlerts.Add(alert);
        await _context.SaveChangesAsync();
        
        var createdAlert = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .FirstOrDefaultAsync(a => a.Id == alert.Id);
        
        if (createdAlert == null) throw new Exception("Failed to create alert");
        
        return alert.ToDto();
    }

    public async Task<InventoryAlertDto?> UpdateAlertAsync(int id, UpdateInventoryAlertDto dto)
    {
        var alert = await _context.InventoryAlerts
            .Include(a => a.Product)
            .ThenInclude(p => p.Category)
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (alert == null) return null;

        alert.UpdateFromDto(dto);
        await _context.SaveChangesAsync();
        return alert.ToDto();
    }

    public async Task<bool> DeleteAlertAsync(int id)
    {
        var alert = await _context.InventoryAlerts.FindAsync(id);
        if (alert == null) return false;

        _context.InventoryAlerts.Remove(alert);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsAcknowledgedAsync(int alertId)
    {
        var alert = await _context.InventoryAlerts.FindAsync(alertId);
        if (alert == null) return false;

        // Check if already acknowledged
        if (alert.AlertStatusId == 2)
            return true; // Already acknowledged

        alert.AlertStatusId = 2; // "Acknowledged" id
        alert.AcknowledgedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsResolvedAsync(int alertId)
    {
        var alert = await _context.InventoryAlerts.FindAsync(alertId);
        if (alert == null) return false;

        // Check if already resolved
        if (alert.AlertStatusId == 3)
            return true; // Already resolved

        alert.AlertStatusId = 3; // "Resolved" id
        alert.ResolvedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsActiveAsync(int alertId)
    {
        var alert = await _context.InventoryAlerts.FindAsync(alertId);
        if (alert == null) return false;

        // Check if already active
        if (alert.AlertStatusId == 1)
            return true; // Already active

        alert.AlertStatusId = 1; // "Active" id
        alert.AcknowledgedAt = null;
        alert.ResolvedAt = null;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AlertStatsDto> GetAlertStatsAsync()
    {
        var alerts = await _context.InventoryAlerts
            .Include(a => a.AlertStatus)
            .Include(a => a.AlertType)
            .ToListAsync();

        // Calculate stats in memory (fast)
        var stats = new AlertStatsDto
        {
            TotalAlerts = alerts.Count,
            ActiveAlerts = alerts.Count(a => a.AlertStatus.Name == "Activa"),
            AcknowledgedAlerts = alerts.Count(a => a.AlertStatus.Name == "Reconocida"),
            ResolvedAlerts = alerts.Count(a => a.AlertStatus.Name == "Resuelta"),
            DiscardedAlerts = alerts.Count(a => a.AlertStatus.Name == "Descartada"),

            // Bonus stats
            HighPriorityAlerts = alerts.Count(a => a.AlertType.Priority == "Alto"),
            MediumPriorityAlerts = alerts.Count(a => a.AlertType.Priority == "Medio"),
            LowPriorityAlerts = alerts.Count(a => a.AlertType.Priority == "Bajo"),

            LowStockAlerts = alerts.Count(a => a.AlertType.Name == "Stock Bajo"),
            OutOfStockAlerts = alerts.Count(a => a.AlertType.Name == "Sin Stock"),

            TodayAlerts = alerts.Count(a => a.CreatedAt.Date == DateTime.UtcNow.Date),
            WeekAlerts = alerts.Count(a => a.CreatedAt >= DateTime.UtcNow.AddDays(-7)),
            MonthAlerts = alerts.Count(a => a.CreatedAt >= DateTime.UtcNow.AddDays(-30))
        };

        return stats;
    }

    public async Task<IEnumerable<AlertsByTypeDto>> GetAlertsByTypeStatsAsync()
    {
        var alertTypeStats = await _context.InventoryAlerts
            .Include(a => a.AlertType)
            .Include(a => a.AlertStatus)
            .GroupBy(a => new
            {
                a.AlertType.Id,
                a.AlertType.Name,
                a.AlertType.Color,
                a.AlertType.Priority
            })
            .Select(g => new AlertsByTypeDto
            {
                AlertTypeId = g.Key.Id,
                AlertTypeName = g.Key.Name,
                AlertTypeColor = g.Key.Color,
                Priority = g.Key.Priority,
                Count = g.Count(),
                ActiveCount = g.Count(a => a.AlertStatus.Name == "Activa"),
                ResolvedCount = g.Count(a => a.AlertStatus.Name == "Resuelta")
            })
            .OrderByDescending(x => x.Count)
            .ToListAsync();

        return alertTypeStats;
    }

    public async Task<int> GenerateStockAlertsAsync()
    {
        // Get products that need alerts
        var lowStockProducts = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .Where(p => p.CurrentStock <= p.MinimumStock)
            .Where(p => !_context.InventoryAlerts.Any(a =>
                a.ProductId == p.Id &&
                a.AlertStatus.Name == "Activa" &&
                (a.AlertType.Name == "Stock Bajo" || a.AlertType.Name == "Sin Stock")))
            .ToListAsync();

        var newAlerts = new List<InventoryAlert>();

        foreach (var product in lowStockProducts)
        {
            InventoryAlert newAlert;

            if (product.CurrentStock <= 0)
            {
                // Out of stock alert
                newAlert = new InventoryAlert
                {
                    ProductId = product.Id,
                    AlertTypeId = 2, // "Out Stock"
                    AlertStatusId = 1, // "Active"
                    Message = $"{product.Name}: Producto completamente agotado - Requiere reposición inmediata",
                    CreatedAt = DateTime.UtcNow
                };
            }
            else
            {
                // Low stock alert
                newAlert = new InventoryAlert
                {
                    ProductId = product.Id,
                    AlertTypeId = 1, // "Low stock"
                    AlertStatusId = 1, // "Active"
                    Message =
                        $"{product.Name}: Stock bajo detectado - Stock actual ({product.CurrentStock}) por debajo del mínimo ({product.MinimumStock})",
                    CreatedAt = DateTime.UtcNow
                };
            }

            newAlerts.Add(newAlert);
        }

        if (newAlerts.Any())
        {
            _context.InventoryAlerts.AddRange(newAlerts);
            await _context.SaveChangesAsync();
        }

        return newAlerts.Count;
    }

    public async Task<bool> CreateLowStockAlertAsync(int productId)
    {
        // Check if product exists and get stock info
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        // Check if there's already an active alert for this product
        var existingAlert = await _context.InventoryAlerts
            .AnyAsync(a => a.ProductId == productId &&
                           a.AlertStatus.Name == "Activa" &&
                           a.AlertType.Name == "Stock Bajo");

        if (existingAlert) return true; // Already has active low stock alert

        var newAlert = new InventoryAlert
        {
            ProductId = productId,
            AlertTypeId = 1, // "Low stock"
            AlertStatusId = 1, // "Active"
            Message =
                $"{product.Name}: Alerta de stock bajo creada - Stock actual ({product.CurrentStock}) por debajo del mínimo ({product.MinimumStock})",
            CreatedAt = DateTime.UtcNow
        };

        _context.InventoryAlerts.Add(newAlert);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CreateOutOfStockAlertAsync(int productId)
    {
        // Check if product exists
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        // Check if there's already an active out of stock alert
        var existingAlert = await _context.InventoryAlerts
            .AnyAsync(a => a.ProductId == productId &&
                           a.AlertStatus.Name == "Activa" &&
                           a.AlertType.Name == "Sin Stock");

        if (existingAlert) return true; // Already has active out of stock alert

        var newAlert = new InventoryAlert
        {
            ProductId = productId,
            AlertTypeId = 2, // "Out stock"
            AlertStatusId = 1, // "Active"
            Message = $"{product.Name}: Producto completamente agotado - Requiere reposición inmediata",
            CreatedAt = DateTime.UtcNow
        };

        _context.InventoryAlerts.Add(newAlert);
        await _context.SaveChangesAsync();
        return true;
    }
}