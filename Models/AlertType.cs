using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestockAPI.Models;

[Table("AlertTypes")]
public class AlertType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Description { get; set; }
    
    [StringLength(7)]
    public string Color { get; set; } = "#6b7280";
    
    [StringLength(20)]
    public string Priority { get; set; } = "Normal";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    // Navigation property
    public virtual ICollection<InventoryAlert> InventoryAlerts { get; set; } = new List<InventoryAlert>();
}