using Microsoft.EntityFrameworkCore;
using RestockAPI.Data.Context;
using RestockAPI.DTOs;
using RestockAPI.DTOs.Extensions;
using RestockAPI.DTOs.UnitType;
using RestockAPI.Services.Interfaces;

namespace RestockAPI.Services;

public class UnitTypeService : IUnitTypeService
{
    private readonly ApplicationDbContext _context;
    
    public UnitTypeService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UnitTypeDto>> GetAllUnitTypesAsync()
    {
        var unitTypes = await _context.UnitTypes.ToListAsync();
        return unitTypes.Select(ut => ut.ToDto());
    }

    public async Task<IEnumerable<UnitTypeDto>> GetActiveUnitTypesAsync()
    {
        var unitTypes = await _context.UnitTypes
            .Where(ut => ut.IsActive)
            .ToListAsync();
        
        return unitTypes.Select(ut => ut.ToDto());
    }

    public async Task<UnitTypeDto?> GetUnitTypeByIdAsync(int id)
    {
        var unitType = await _context.UnitTypes
            .FirstOrDefaultAsync(ut => ut.Id == id);

        return unitType?.ToDto();
    }

    public async Task<IEnumerable<UnitTypeSummaryDto>> GetUnitTypeSummariesAsync()
    {
        var unitTypes = await _context.UnitTypes.ToListAsync();
        return unitTypes.Select(ut => ut.ToSummaryDto());
    }

    public async Task<IEnumerable<UnitTypeSummaryDto>> GetActiveUnitTypeSummariesAsync()
    {
        var unitTypes = await _context.UnitTypes
            .Where(ut => ut.IsActive)
            .ToListAsync();
        
        return unitTypes.Select(ut => ut.ToSummaryDto());
    }

    public async Task<UnitTypeDto> CreateUnitTypeAsync(CreateUnitTypeDto dto)
    {
        
        var unitType = dto.ToEntity();
        _context.UnitTypes.Add(unitType);
        await _context.SaveChangesAsync();

        var createdUnitType = await _context.UnitTypes
            .FirstOrDefaultAsync(ut => ut.Id == unitType.Id);

        return createdUnitType!.ToDto();
    }

    public async Task<UnitTypeDto?> UpdateUnitTypeAsync(int id, UpdateUnitTypeDto dto)
    {
        var unitType = await _context.UnitTypes.FindAsync(id);
        if (unitType == null) return null;
        
        unitType.UpdateFromDto(dto);
        await _context.SaveChangesAsync();
        
        var createdUnitType = await _context.UnitTypes
            .FirstOrDefaultAsync(ut => ut.Id == unitType.Id);

        return createdUnitType!.ToDto();
    }

    public async Task<bool> DeleteUnitTypeAsync(int id)
    {
        var unitType = await _context.UnitTypes.FirstOrDefaultAsync(ut => ut.Id == id);
        if(unitType == null) return false;

        _context.UnitTypes.Remove(unitType);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ToggleUnitTypeActiveAsync(int id)
    {
        var unitType = await _context.UnitTypes.FirstOrDefaultAsync(ut => ut.Id == id);
        if (unitType == null) return false;
        
        unitType.IsActive = !unitType.IsActive;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<ProductSummaryDto>> GetProductsByUnitTypeAsync(int unitTypeId)
    {
        var products = await _context.Products
            .Where(p => p.UnitId == unitTypeId)
            .ToListAsync();
        
        return products.Select(p => p.ToSummaryDto());
    }

    public async Task<int> GetActiveProductCountByUnitTypeAsync(int unitTypeId)
    {
        var products = await _context.Products
            .Where(p => p.UnitId == unitTypeId && p.IsActive)
            .ToListAsync();
        
        return products.Count();
    }

    public async Task<decimal> GetTotalStockValueByUnitTypeAsync(int unitTypeId)
    {
        var products = await _context.Products
            .Where(p => p.UnitId == unitTypeId && p.IsActive)
            .ToListAsync();
        
        return products.Sum(p => p.CurrentStock * p.Price ?? 0);
    }

    public async Task<IEnumerable<UnitTypeDto>> SearchUnitTypesAsync(string searchTerm)
    {
        var unitTypes = await _context.UnitTypes
            .Where(ut => ut.IsActive && 
                (ut.Name.ToLower().Contains(searchTerm.ToLower())
                || ut.Description.ToLower().Contains(searchTerm.ToLower())
                || ut.Symbol.ToLower().Contains(searchTerm.ToLower()))
             )
            .OrderBy(ut => ut.Name)
            .ToListAsync();
        
        return unitTypes.Select(ut => ut.ToDto());
    }
}