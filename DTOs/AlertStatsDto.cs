namespace RestockAPI.DTOs;

public class AlertStatsDto
{
    public int TotalAlerts { get; set; }
    public int ActiveAlerts { get; set; }
    public int AcknowledgedAlerts { get; set; }
    public int ResolvedAlerts { get; set; }
    public int DiscardedAlerts { get; set; }
    
    public int HighPriorityAlerts { get; set; }
    public int MediumPriorityAlerts { get; set; }
    public int LowPriorityAlerts { get; set; }
    
    public int LowStockAlerts { get; set; }
    public int OutOfStockAlerts { get; set; }
    public int ExpiringAlerts { get; set; }
    
    public int TodayAlerts { get; set; }
    public int WeekAlerts { get; set; }
    public int MonthAlerts { get; set; }
    
    public decimal AlertResolutionRate { get; set; } // Percentage of resolved alerts
    public decimal AverageResolutionTimeHours { get; set; }
}