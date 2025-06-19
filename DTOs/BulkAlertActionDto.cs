using System.ComponentModel.DataAnnotations;

namespace RestockAPI.DTOs;

public class BulkAlertActionDto
{
    [Required]
    public IEnumerable<int> AlertIds { get; set; } = new List<int>();
    public string? Reason { get; set; }
}