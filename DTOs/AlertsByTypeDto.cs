namespace RestockAPI.DTOs;

public class AlertsByTypeDto
{
    public int AlertTypeId { get; set; }
    public string AlertTypeName { get; set; } = string.Empty;
    public string AlertTypeColor { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public int Count { get; set; }
    public int ActiveCount { get; set; }
    public int ResolvedCount { get; set; }
}