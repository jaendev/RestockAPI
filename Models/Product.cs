using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestockAPI.Models;

namespace RestockAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal CurrentStock { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal MinimumStock { get; set; }
    
    // Foreign Key for UnitType
    [Required]
    [ForeignKey("Unit")]
    public int UnitId { get; set; }
    
    // Navigation Property for UnitType - Remove default value
    public virtual UnitType Unit { get; set; } = null!;
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; set; }  // Make nullable if optional
    
    public string? ImageUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Foreign Key for Category
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    
    // Navigation Property for Category
    public virtual Category Category { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Computed Properties
    [NotMapped]
    public bool IsLowStock => CurrentStock <= MinimumStock;
    
    [NotMapped]
    public bool IsOutOfStock => CurrentStock <= 0;
    
    [NotMapped]
    public decimal TotalValue => CurrentStock * (Price ?? 0);  // Handle nullable Price
    
    [NotMapped]
    public int DaysOld => (DateTime.UtcNow - CreatedAt).Days;
    
    [NotMapped]
    public string StockStatus => IsOutOfStock ? "Sin Stock" : 
        IsLowStock ? "Bajo" : 
        "Normal";
    
    // This will now use the Unit navigation property
    [NotMapped]
    public string UnitDisplay => Unit?.Symbol ?? "units";
}