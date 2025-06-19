namespace RestockAPI.DTOs;

public class AlertsByProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    public int AlertCount { get; set; }
    public int ActiveAlertCount { get; set; }
    public string MostCommonAlertType { get; set; } = string.Empty;
    public DateTime? LastAlertDate { get; set; }
}