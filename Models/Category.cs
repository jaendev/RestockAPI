using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestockAPI.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(7)]
    public string Color { get; set; } // <- Hex
    
    [Required]
    public bool IsActive { get; set; } = true;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
 
}