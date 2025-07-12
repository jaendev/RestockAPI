namespace RestockAPI.DTOs.UnitType;

public class UnitTypeSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    // Useful for dropdowns
    public string DisplayText => $"{Name} ({Symbol})";

}