namespace RestockAPI.DTOs;

public class BulkAlertResultDto
{
    public int ProcessedCount { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}