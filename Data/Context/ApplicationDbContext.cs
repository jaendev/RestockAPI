using System.Drawing;
using Microsoft.EntityFrameworkCore;
using RestockAPI.Models;

namespace RestockAPI.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InventoryAlert> InventoryAlerts { get; set; }
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<AlertType> AlertTypes { get; set; }
    public DbSet<AlertStatus> AlertStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================================
        // PRODUCT CONFIGURATION
        // ================================
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            entity.Property(p => p.CurrentStock)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            entity.Property(p => p.MinimumStock)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            entity.Property(p => p.UnitId)
                .IsRequired()
                .HasDefaultValue(1);

            entity.Property(p => p.Price)
                .IsRequired(false)
                .HasColumnType("decimal(10,2)");

            entity.Property(p => p.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(500);

            entity.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(p => p.CategoryId)
                .IsRequired();

            entity.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");

            entity.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        });

        // ================================
        // CATEGORY CONFIGURATION
        // ================================
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            entity.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(7)
                .HasDefaultValue("#6366f1");

            entity.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        });

        // ================================
        // UNITTYPE CONFIGURATION
        // ================================
        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(u => u.Symbol)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(u => u.Description)
                .IsRequired(false)
                .HasMaxLength(200);

            entity.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        });

        // ================================
        // ALERTTYPE CONFIGURATION
        // ================================
        modelBuilder.Entity<AlertType>(entity =>
        {
            entity.HasKey(at => at.Id);

            entity.Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(at => at.Description)
                .IsRequired(false)
                .HasMaxLength(200);

            entity.Property(at => at.Color)
                .IsRequired()
                .HasMaxLength(7)
                .HasDefaultValue("#6b7280");

            entity.Property(at => at.Priority)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Normal");

            entity.Property(at => at.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(at => at.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        });

        // ================================
        // ALERTSTATUS CONFIGURATION
        // ================================
        modelBuilder.Entity<AlertStatus>(entity =>
        {
            entity.HasKey(als => als.Id);

            entity.Property(als => als.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(als => als.Description)
                .IsRequired(false)
                .HasMaxLength(200);

            entity.Property(als => als.Color)
                .IsRequired()
                .HasMaxLength(7)
                .HasDefaultValue("#6b7280");

            entity.Property(als => als.IsFinalState)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(als => als.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(als => als.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");
        });

        // ================================
        // INVENTORYALERT CONFIGURATION
        // ================================
        modelBuilder.Entity<InventoryAlert>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.ProductId)
                .IsRequired();

            entity.Property(a => a.AlertTypeId)
                .IsRequired();

            entity.Property(a => a.AlertStatusId)
                .IsRequired()
                .HasDefaultValue(1); // Default to Active

            entity.Property(a => a.Message)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(a => a.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");

            entity.Property(a => a.AcknowledgedAt)
                .IsRequired(false);

            entity.Property(a => a.ResolvedAt)
                .IsRequired(false);
        });

        // ================================
        // RELATIONSHIPS
        // ================================

        // Product -> Category (Many to One)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Product -> UnitType (Many to One)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Unit)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        // InventoryAlert -> Product (Many to One)
        modelBuilder.Entity<InventoryAlert>()
            .HasOne(a => a.Product)
            .WithMany()
            .HasForeignKey(a => a.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // InventoryAlert -> AlertType (Many to One)
        modelBuilder.Entity<InventoryAlert>()
            .HasOne(a => a.AlertType)
            .WithMany(at => at.InventoryAlerts)
            .HasForeignKey(a => a.AlertTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // InventoryAlert -> AlertStatus (Many to One)
        modelBuilder.Entity<InventoryAlert>()
            .HasOne(a => a.AlertStatus)
            .WithMany(als => als.InventoryAlerts)
            .HasForeignKey(a => a.AlertStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // ================================
        // INDEXES FOR PERFORMANCE
        // ================================

        // Product Indexes
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Products_Name");

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.CategoryId)
            .HasDatabaseName("IX_Products_CategoryId");

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.UnitId)
            .HasDatabaseName("IX_Products_UnitId");

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.IsActive)
            .HasDatabaseName("IX_Products_IsActive");

        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.IsActive, p.CategoryId })
            .HasDatabaseName("IX_Products_IsActive_CategoryId");

        // InventoryAlert Indexes
        modelBuilder.Entity<InventoryAlert>()
            .HasIndex(a => a.ProductId)
            .HasDatabaseName("IX_InventoryAlerts_ProductId");

        modelBuilder.Entity<InventoryAlert>()
            .HasIndex(a => a.AlertTypeId)
            .HasDatabaseName("IX_InventoryAlerts_AlertTypeId");

        modelBuilder.Entity<InventoryAlert>()
            .HasIndex(a => a.AlertStatusId)
            .HasDatabaseName("IX_InventoryAlerts_AlertStatusId");

        modelBuilder.Entity<InventoryAlert>()
            .HasIndex(a => a.CreatedAt)
            .HasDatabaseName("IX_InventoryAlerts_CreatedAt");

        modelBuilder.Entity<InventoryAlert>()
            .HasIndex(a => new { a.AlertStatusId, a.AlertTypeId })
            .HasDatabaseName("IX_InventoryAlerts_Status_Type");

        // Category Indexes
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .HasDatabaseName("IX_Categories_Name");

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.IsActive)
            .HasDatabaseName("IX_Categories_IsActive");

        // UnitType Indexes
        modelBuilder.Entity<UnitType>()
            .HasIndex(u => u.Name)
            .HasDatabaseName("IX_UnitTypes_Name");

        modelBuilder.Entity<UnitType>()
            .HasIndex(u => u.IsActive)
            .HasDatabaseName("IX_UnitTypes_IsActive");

        // AlertType Indexes
        modelBuilder.Entity<AlertType>()
            .HasIndex(at => at.Name)
            .HasDatabaseName("IX_AlertTypes_Name");

        // AlertStatus Indexes
        modelBuilder.Entity<AlertStatus>()
            .HasIndex(als => als.Name)
            .HasDatabaseName("IX_AlertStatuses_Name");

        // ================================
        // CHECK CONSTRAINTS
        // ================================

        modelBuilder.Entity<Product>()
            .HasCheckConstraint("CK_Product_CurrentStock_NonNegative",
                "\"CurrentStock\" >= 0");

        modelBuilder.Entity<Product>()
            .HasCheckConstraint("CK_Product_MinimumStock_NonNegative",
                "\"MinimumStock\" >= 0");

        modelBuilder.Entity<Product>()
            .HasCheckConstraint("CK_Product_Price_NonNegative",
                "\"Price\" IS NULL OR \"Price\" >= 0");

        // ================================
        // DATOS SEMILLA (SEED DATA)
        // ================================

        var fechaSemilla = new DateTime(2025, 6, 14, 12, 0, 0, DateTimeKind.Utc);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1, Name = "Alimentos", Description = "Productos alimenticios y comestibles", Color = "#22c55e",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 2, Name = "Bebidas", Description = "Bebidas y refrescos", Color = "#06b6d4", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 3, Name = "Limpieza", Description = "Productos de limpieza para el hogar", Color = "#3b82f6",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 4, Name = "Higiene Personal", Description = "Productos de higiene y cuidado personal",
                Color = "#f59e0b", IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 5, Name = "Medicinas", Description = "Medicamentos y productos farmacéuticos", Color = "#ef4444",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 6, Name = "Hogar", Description = "Artículos para el hogar y decoración", Color = "#8b5cf6",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 7, Name = "Mascotas", Description = "Productos para mascotas y animales", Color = "#10b981",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new Category
            {
                Id = 8, Name = "Otros", Description = "Productos varios sin categoría específica", Color = "#6b7280",
                IsActive = true, CreatedAt = fechaSemilla
            }
        );

        // Seed UnitTypes
        modelBuilder.Entity<UnitType>().HasData(
            new UnitType
            {
                Id = 1, Name = "Unidades", Symbol = "un", Description = "Productos por unidad individual",
                IsActive = true, CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 2, Name = "Kilogramos", Symbol = "kg", Description = "Peso en kilogramos", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 3, Name = "Gramos", Symbol = "g", Description = "Peso en gramos", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 4, Name = "Litros", Symbol = "L", Description = "Volumen en litros", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 5, Name = "Mililitros", Symbol = "mL", Description = "Volumen en mililitros", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 6, Name = "Paquetes", Symbol = "paq", Description = "Productos en paquetes", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 7, Name = "Cajas", Symbol = "cajas", Description = "Productos en cajas", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 8, Name = "Botellas", Symbol = "bot", Description = "Productos en botellas", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 9, Name = "Tubos", Symbol = "tubos", Description = "Productos en tubos", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 10, Name = "Docenas", Symbol = "doc", Description = "Productos por docenas", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 11, Name = "Latas", Symbol = "latas", Description = "Productos en latas", IsActive = true,
                CreatedAt = fechaSemilla
            },
            new UnitType
            {
                Id = 12, Name = "Sobres", Symbol = "sobres", Description = "Productos en sobres", IsActive = true,
                CreatedAt = fechaSemilla
            }
        );

        // Seed AlertTypes
        modelBuilder.Entity<AlertType>().HasData(
            new AlertType
            {
                Id = 1, Name = "Stock Bajo",
                Description = "El stock del producto está por debajo del mínimo establecido", Color = "#f59e0b",
                Priority = "Medio", IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertType
            {
                Id = 2, Name = "Sin Stock", Description = "El producto está completamente agotado", Color = "#ef4444",
                Priority = "Alto", IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertType
            {
                Id = 3, Name = "Próximo a Vencer", Description = "El producto está cerca de su fecha de vencimiento",
                Color = "#f97316", Priority = "Bajo", IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertType
            {
                Id = 4, Name = "Exceso de Stock", Description = "El producto tiene stock excesivamente alto",
                Color = "#8b5cf6", Priority = "Bajo", IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertType
            {
                Id = 5, Name = "Precio Cambiado", Description = "El precio del producto ha sido modificado",
                Color = "#06b6d4", Priority = "Informativo", IsActive = true, CreatedAt = fechaSemilla
            }
        );

        // Seed AlertStatus
        modelBuilder.Entity<AlertStatus>().HasData(
            new AlertStatus
            {
                Id = 1, Name = "Activa", Description = "La alerta está activa y requiere atención", Color = "#f59e0b",
                IsFinalState = false, IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertStatus
            {
                Id = 2, Name = "Reconocida", Description = "La alerta ha sido vista y reconocida por el usuario",
                Color = "#3b82f6", IsFinalState = false, IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertStatus
            {
                Id = 3, Name = "Resuelta", Description = "La alerta ha sido resuelta y está cerrada", Color = "#22c55e",
                IsFinalState = true, IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertStatus
            {
                Id = 4, Name = "Descartada", Description = "La alerta ha sido descartada sin acción", Color = "#6b7280",
                IsFinalState = true, IsActive = true, CreatedAt = fechaSemilla
            },
            new AlertStatus
            {
                Id = 5, Name = "En Proceso", Description = "La alerta está siendo atendida actualmente",
                Color = "#8b5cf6", IsFinalState = false, IsActive = true, CreatedAt = fechaSemilla
            }
        );
    }
}