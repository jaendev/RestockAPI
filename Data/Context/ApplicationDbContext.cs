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

        // ================================
        // SEED DATA - PRODUCTS
        // ================================

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            // FOOD (CategoryId = 1)
            new Product
            {
                Id = 1, Name = "Arroz Blanco", Description = "Arroz blanco de grano largo 1kg", CurrentStock = 25,
                MinimumStock = 10, UnitId = 2, Price = 3.50m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 2, Name = "Aceite de Oliva", Description = "Aceite de oliva virgen extra 500ml", CurrentStock = 8,
                MinimumStock = 5, UnitId = 8, Price = 8.90m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 3, Name = "Pan de Molde", Description = "Pan de molde integral 500g", CurrentStock = 2,
                MinimumStock = 5, UnitId = 1, Price = 1.80m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 4, Name = "Huevos Frescos", Description = "Huevos frescos de granja", CurrentStock = 0,
                MinimumStock = 3, UnitId = 10, Price = 4.20m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock
            new Product
            {
                Id = 5, Name = "Leche Entera", Description = "Leche entera pasteurizada 1L", CurrentStock = 15,
                MinimumStock = 8, UnitId = 4, Price = 2.50m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 6, Name = "Azúcar Blanca", Description = "Azúcar refinada 1kg", CurrentStock = 1, MinimumStock = 4,
                UnitId = 2, Price = 2.20m, CategoryId = 1, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock

            // DRINKS (CategoryId = 2)
            new Product
            {
                Id = 7, Name = "Agua Mineral", Description = "Agua mineral natural 1.5L", CurrentStock = 30,
                MinimumStock = 12, UnitId = 8, Price = 1.20m, CategoryId = 2, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 8, Name = "Coca Cola", Description = "Refresco de cola 2L", CurrentStock = 5, MinimumStock = 6,
                UnitId = 8, Price = 3.50m, CategoryId = 2, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 9, Name = "Jugo de Naranja", Description = "Jugo natural de naranja 1L", CurrentStock = 8,
                MinimumStock = 4, UnitId = 7, Price = 2.80m, CategoryId = 2, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 10, Name = "Cerveza Artesanal", Description = "Cerveza artesanal 330ml", CurrentStock = 0,
                MinimumStock = 6, UnitId = 8, Price = 2.50m, CategoryId = 2, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock
            new Product
            {
                Id = 11, Name = "Café Molido", Description = "Café molido natural 500g", CurrentStock = 12,
                MinimumStock = 5, UnitId = 6, Price = 7.50m, CategoryId = 2, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },

            // CLEANING (CategoryId = 3)
            new Product
            {
                Id = 12, Name = "Detergente Líquido", Description = "Detergente para ropa 2L", CurrentStock = 6,
                MinimumStock = 3, UnitId = 8, Price = 12.50m, CategoryId = 3, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 13, Name = "Papel Higiénico", Description = "Papel higiénico suave 12 rollos", CurrentStock = 1,
                MinimumStock = 2, UnitId = 6, Price = 8.90m, CategoryId = 3, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 14, Name = "Limpiavidrios", Description = "Spray limpiavidrios 500ml", CurrentStock = 4,
                MinimumStock = 2, UnitId = 8, Price = 4.50m, CategoryId = 3, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 15, Name = "Lejía", Description = "Lejía desinfectante 1L", CurrentStock = 0, MinimumStock = 3,
                UnitId = 8, Price = 2.50m, CategoryId = 3, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock
            new Product
            {
                Id = 16, Name = "Esponjas Cocina", Description = "Esponjas de cocina pack x6", CurrentStock = 8,
                MinimumStock = 4, UnitId = 6, Price = 3.20m, CategoryId = 3, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },

            // PERSONAL HYGIENE (CategoryId = 4)
            new Product
            {
                Id = 17, Name = "Champú", Description = "Champú para cabello normal 400ml", CurrentStock = 5,
                MinimumStock = 3, UnitId = 8, Price = 6.80m, CategoryId = 4, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 18, Name = "Pasta Dental", Description = "Pasta dental con flúor 100ml", CurrentStock = 2,
                MinimumStock = 4, UnitId = 9, Price = 3.20m, CategoryId = 4, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 19, Name = "Jabón de Manos", Description = "Jabón líquido antibacterial 250ml", CurrentStock = 7,
                MinimumStock = 3, UnitId = 8, Price = 4.90m, CategoryId = 4, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 20, Name = "Desodorante", Description = "Desodorante en aerosol 150ml", CurrentStock = 0,
                MinimumStock = 2, UnitId = 1, Price = 5.50m, CategoryId = 4, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock
            new Product
            {
                Id = 21, Name = "Gel de Ducha", Description = "Gel de ducha hidratante 500ml", CurrentStock = 9,
                MinimumStock = 4, UnitId = 8, Price = 6.20m, CategoryId = 4, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },

            // MEDICINES (CategoryId = 5)
            new Product
            {
                Id = 22, Name = "Paracetamol 500mg", Description = "Analgésico y antifebril", CurrentStock = 45,
                MinimumStock = 20, UnitId = 1, Price = 0.15m, CategoryId = 5, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 23, Name = "Alcohol 70%", Description = "Alcohol etílico desinfectante 250ml", CurrentStock = 3,
                MinimumStock = 5, UnitId = 8, Price = 2.50m, CategoryId = 5, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 24, Name = "Vendas Elásticas", Description = "Vendas elásticas 5cm x 4.5m", CurrentStock = 8,
                MinimumStock = 4, UnitId = 1, Price = 3.80m, CategoryId = 5, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 25, Name = "Ibuprofeno 400mg", Description = "Antiinflamatorio", CurrentStock = 0,
                MinimumStock = 15, UnitId = 1, Price = 0.20m, CategoryId = 5, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock
            new Product
            {
                Id = 26, Name = "Vitamina C", Description = "Suplemento vitamina C 1000mg", CurrentStock = 25,
                MinimumStock = 10, UnitId = 1, Price = 0.25m, CategoryId = 5, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },

            // HOME (CategoryId = 6)
            new Product
            {
                Id = 27, Name = "Bombillas LED", Description = "Bombilla LED 10W luz cálida", CurrentStock = 8,
                MinimumStock = 5, UnitId = 1, Price = 4.50m, CategoryId = 6, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 28, Name = "Pilas AA", Description = "Pilas alcalinas AA pack x4", CurrentStock = 2,
                MinimumStock = 6, UnitId = 6, Price = 6.80m, CategoryId = 6, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 29, Name = "Velas Aromáticas", Description = "Velas aromáticas de lavanda", CurrentStock = 12,
                MinimumStock = 4, UnitId = 1, Price = 8.90m, CategoryId = 6, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 30, Name = "Papel Aluminio", Description = "Rollo papel aluminio 25m", CurrentStock = 0,
                MinimumStock = 3, UnitId = 1, Price = 3.20m, CategoryId = 6, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Out stock

            // PEETS (CategoryId = 7)
            new Product
            {
                Id = 31, Name = "Comida para Perros", Description = "Alimento balanceado para perros 3kg",
                CurrentStock = 5, MinimumStock = 3, UnitId = 2, Price = 15.50m, CategoryId = 7, IsActive = true,
                CreatedAt = fechaSemilla, UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 32, Name = "Arena para Gatos", Description = "Arena sanitaria para gatos 5kg", CurrentStock = 1,
                MinimumStock = 4, UnitId = 2, Price = 8.90m, CategoryId = 7, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low stock
            new Product
            {
                Id = 33, Name = "Juguete para Perros", Description = "Pelota de goma resistente", CurrentStock = 8,
                MinimumStock = 3, UnitId = 1, Price = 6.50m, CategoryId = 7, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 34, Name = "Shampoo para Mascotas", Description = "Shampoo neutro para perros y gatos 500ml",
                CurrentStock = 0, MinimumStock = 2, UnitId = 8, Price = 12.80m, CategoryId = 7, IsActive = true,
                CreatedAt = fechaSemilla, UpdatedAt = fechaSemilla
            }, // Out stock

            // OTHERS (CategoryId = 8)
            new Product
            {
                Id = 35, Name = "Cargador USB", Description = "Cargador universal USB tipo C", CurrentStock = 4,
                MinimumStock = 2, UnitId = 1, Price = 12.50m, CategoryId = 8, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 36, Name = "Paraguas", Description = "Paraguas compacto resistente al viento", CurrentStock = 1,
                MinimumStock = 3, UnitId = 1, Price = 18.90m, CategoryId = 8, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            }, // Low Stock
            new Product
            {
                Id = 37, Name = "Linterna LED", Description = "Linterna LED recargable", CurrentStock = 6,
                MinimumStock = 2, UnitId = 1, Price = 15.20m, CategoryId = 8, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            },
            new Product
            {
                Id = 38, Name = "Mascarillas", Description = "Mascarillas desechables caja x50", CurrentStock = 0,
                MinimumStock = 5, UnitId = 7, Price = 8.50m, CategoryId = 8, IsActive = true, CreatedAt = fechaSemilla,
                UpdatedAt = fechaSemilla
            } // Out stock
        );

        // ================================
        // SEED DATA - ALERTS OF INVENTORY
        // ================================

        // Seed InventoryAlerts (for products with low stock or no stock)
        modelBuilder.Entity<InventoryAlert>().HasData(
            // ALERT OF LOW STOCK
            new InventoryAlert
            {
                Id = 1, ProductId = 3, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Pan de Molde: Stock actual (2) por debajo del mínimo (5)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 2, ProductId = 6, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Azúcar Blanca: Stock actual (1) por debajo del mínimo (4)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 3, ProductId = 8, AlertTypeId = 1, AlertStatusId = 2,
                Message = "Coca Cola: Stock actual (5) por debajo del mínimo (6)",
                CreatedAt = fechaSemilla.AddHours(-2), AcknowledgedAt = fechaSemilla.AddHours(-1)
            },
            new InventoryAlert
            {
                Id = 4, ProductId = 13, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Papel Higiénico: Stock actual (1) por debajo del mínimo (2)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 5, ProductId = 18, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Pasta Dental: Stock actual (2) por debajo del mínimo (4)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 6, ProductId = 23, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Alcohol 70%: Stock actual (3) por debajo del mínimo (5)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 7, ProductId = 28, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Pilas AA: Stock actual (2) por debajo del mínimo (6)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 8, ProductId = 32, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Arena para Gatos: Stock actual (1) por debajo del mínimo (4)", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 9, ProductId = 36, AlertTypeId = 1, AlertStatusId = 1,
                Message = "Paraguas: Stock actual (1) por debajo del mínimo (3)", CreatedAt = fechaSemilla
            },

            // ALERT OF NO STOCK
            new InventoryAlert
            {
                Id = 10, ProductId = 4, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Huevos Frescos: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 11, ProductId = 10, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Cerveza Artesanal: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 12, ProductId = 15, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Lejía: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 13, ProductId = 20, AlertTypeId = 2, AlertStatusId = 2,
                Message = "Desodorante: Producto completamente agotado", CreatedAt = fechaSemilla.AddHours(-3),
                AcknowledgedAt = fechaSemilla.AddHours(-2)
            },
            new InventoryAlert
            {
                Id = 14, ProductId = 25, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Ibuprofeno 400mg: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 15, ProductId = 30, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Papel Aluminio: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 16, ProductId = 34, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Shampoo para Mascotas: Producto completamente agotado", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 17, ProductId = 38, AlertTypeId = 2, AlertStatusId = 1,
                Message = "Mascarillas: Producto completamente agotado", CreatedAt = fechaSemilla
            },

            // ALERTS OF EXPIRING SOON (simulated)
            new InventoryAlert
            {
                Id = 18, ProductId = 5, AlertTypeId = 3, AlertStatusId = 3,
                Message = "Leche Entera: Próxima a vencer en 3 días", CreatedAt = fechaSemilla.AddDays(-2),
                AcknowledgedAt = fechaSemilla.AddDays(-1), ResolvedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 19, ProductId = 9, AlertTypeId = 3, AlertStatusId = 1,
                Message = "Jugo de Naranja: Próximo a vencer en 2 días", CreatedAt = fechaSemilla
            },
            new InventoryAlert
            {
                Id = 20, ProductId = 22, AlertTypeId = 3, AlertStatusId = 2,
                Message = "Paracetamol 500mg: Próximo a vencer en 30 días", CreatedAt = fechaSemilla.AddHours(-4),
                AcknowledgedAt = fechaSemilla.AddHours(-3)
            },

            // ALERTS OF EXCESSIVE STOCK
            new InventoryAlert
            {
                Id = 21, ProductId = 7, AlertTypeId = 4, AlertStatusId = 4,
                Message = "Agua Mineral: Stock excesivo detectado (30 unidades)", CreatedAt = fechaSemilla.AddDays(-1),
                AcknowledgedAt = fechaSemilla.AddHours(-12)
            },

            // ALERTS OF PRICE CHANGED
            new InventoryAlert
            {
                Id = 22, ProductId = 11, AlertTypeId = 5, AlertStatusId = 3,
                Message = "Café Molido: Precio actualizado de €6.80 a €7.50", CreatedAt = fechaSemilla.AddHours(-6),
                AcknowledgedAt = fechaSemilla.AddHours(-5), ResolvedAt = fechaSemilla.AddHours(-4)
            },
            new InventoryAlert
            {
                Id = 23, ProductId = 27, AlertTypeId = 5, AlertStatusId = 2,
                Message = "Bombillas LED: Precio actualizado de €3.90 a €4.50", CreatedAt = fechaSemilla.AddHours(-1),
                AcknowledgedAt = fechaSemilla.AddMinutes(-30)
            }
        );
    }
}