using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestockAPI.Models;

namespace RestockAPI.Models;

[Table("InventoryAlerts")]
public class InventoryAlert
{
    [Key]
    public int Id { get; set; }

    // Foreign Key to Product
    [Required]
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    
    // Foreign Key to AlertType
    [Required]
    [ForeignKey("AlertType")]
    public int AlertTypeId { get; set; }

    // Foreign Key to AlertStatus
    [Required]
    [ForeignKey("AlertStatus")]
    public int AlertStatusId { get; set; } = 1; // Default to Active (assuming ID 1)

    // Descriptive message for the alert
    [Required]
    [StringLength(500)]
    public string Message { get; set; } = string.Empty;

    // When the alert was created
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // When the alert was acknowledged by user
    public DateTime? AcknowledgedAt { get; set; }

    // When the alert was resolved
    public DateTime? ResolvedAt { get; set; }

    // Navigation Properties
    public virtual Product Product { get; set; } = null!;
    public virtual AlertType AlertType { get; set; } = null!;
    public virtual AlertStatus AlertStatus { get; set; } = null!;

    // Computed Properties
    [NotMapped]
    public bool IsActive => AlertStatus?.Name == "Active";

    [NotMapped]
    public bool IsResolved => AlertStatus?.Name == "Resolved";

    [NotMapped]
    public bool IsAcknowledged => AlertStatus?.Name == "Acknowledged";

    [NotMapped]
    public int DaysOld => (DateTime.UtcNow - CreatedAt).Days;

    [NotMapped]
    public int HoursOld => (DateTime.UtcNow - CreatedAt).Hours;

    [NotMapped]
    public string TimeAgo => DaysOld switch
    {
        0 when HoursOld < 1 => "Just now",
        0 when HoursOld == 1 => "1 hour ago",
        0 => $"{HoursOld} hours ago",
        1 => "1 day ago",
        _ => $"{DaysOld} days ago"
    };

    [NotMapped]
    public string Priority => AlertType?.Priority ?? "Normal";
}