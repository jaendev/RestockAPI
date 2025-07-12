using RestockAPI.DTOs;
using RestockAPI.DTOs.UnitType;

namespace RestockAPI.Services.Interfaces;

public interface IUnitTypeService
{
    Task<IEnumerable<UnitTypeDto>> GetAllUnitTypesAsync();
    Task<IEnumerable<UnitTypeDto>> GetActiveUnitTypesAsync();
    Task<UnitTypeDto?> GetUnitTypeByIdAsync(int id);
    Task<IEnumerable<UnitTypeSummaryDto>> GetUnitTypeSummariesAsync();
    Task<IEnumerable<UnitTypeSummaryDto>> GetActiveUnitTypeSummariesAsync(); // For dropdowns
    
    Task<UnitTypeDto> CreateUnitTypeAsync(CreateUnitTypeDto dto);
    Task<UnitTypeDto?> UpdateUnitTypeAsync(int id, UpdateUnitTypeDto dto);
    Task<bool> DeleteUnitTypeAsync(int id);
    Task<bool> ToggleUnitTypeActiveAsync(int id);
    
    Task<IEnumerable<ProductSummaryDto>> GetProductsByUnitTypeAsync(int unitTypeId);
    Task<int> GetActiveProductCountByUnitTypeAsync(int unitTypeId);
    Task<decimal> GetTotalStockValueByUnitTypeAsync(int unitTypeId);

    Task<IEnumerable<UnitTypeDto>> SearchUnitTypesAsync(string searchTerm);
}