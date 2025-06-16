using RestockAPI.Models;

namespace RestockAPI.Services.Interfaces;

public interface IAlertService
{
    public Task<List<InventoryAlert>> GetAlertsActive() { throw new NotImplementedException(); }
    public Task<List<InventoryAlert>> MarckAsRead() { throw new NotImplementedException(); }
    public Task<List<InventoryAlert>> ResolveAlerts() { throw new NotImplementedException(); }
}