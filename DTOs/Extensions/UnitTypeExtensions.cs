
using RestockAPI.DTOs.UnitType;
using RestockAPI.Models;

public static class UnitTypeExtensions
{
    public static UnitTypeDto ToDto(this UnitType unitType)
    {
        return new UnitTypeDto
        {
            Id = unitType.Id,
            Name = unitType.Name,
            Symbol = unitType.Symbol,
            Description = unitType.Description,
            IsActive = unitType.IsActive,
            CreatedAt = unitType.CreatedAt
        };
    }

    public static UnitTypeSummaryDto ToSummaryDto(this UnitType unitType)
    {
        return new UnitTypeSummaryDto
        {
            Id = unitType.Id,
            Name = unitType.Name,
            Symbol = unitType.Symbol,
            IsActive = unitType.IsActive
        };
    }

    public static UnitType ToEntity(this CreateUnitTypeDto dto)
    {
        return new UnitType
        {
            Name = dto.Name,
            Symbol = dto.Symbol,
            Description = dto.Description,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static UnitType UpdateFromDto(this UnitType entity, UpdateUnitTypeDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name))
            entity.Name = dto.Name.Trim();

        if (!string.IsNullOrWhiteSpace(dto.Symbol))
            entity.Symbol = dto.Symbol.Trim();

        if (dto.Description != null)
            entity.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();

        if (dto.IsActive.HasValue)
            entity.IsActive = dto.IsActive.Value;

        return entity;
    }
}