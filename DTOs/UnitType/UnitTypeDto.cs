namespace RestockAPI.DTOs.UnitType;

public class UnitTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Computed properties
    public string DisplayName => $"{Name} ({Symbol})";
    public int DaysOld => (DateTime.UtcNow - CreatedAt).Days;
}