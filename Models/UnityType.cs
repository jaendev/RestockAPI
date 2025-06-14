using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestockAPI.Models;

[Table("UnitTypes")]
public class UnitType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(10)]
    public string Symbol { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Description { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}