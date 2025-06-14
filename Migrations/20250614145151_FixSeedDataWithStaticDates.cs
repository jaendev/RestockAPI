using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestockAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataWithStaticDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, defaultValue: "#6b7280"),
                    IsFinalState = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlertTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, defaultValue: "#6b7280"),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Normal"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, defaultValue: "#6366f1"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CurrentStock = table.Column<decimal>(type: "numeric(10,2)", nullable: false, defaultValue: 0m),
                    MinimumStock = table.Column<decimal>(type: "numeric(10,2)", nullable: false, defaultValue: 0m),
                    UnitId = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("CK_Product_CurrentStock_NonNegative", "\"CurrentStock\" >= 0");
                    table.CheckConstraint("CK_Product_MinimumStock_NonNegative", "\"MinimumStock\" >= 0");
                    table.CheckConstraint("CK_Product_Price_NonNegative", "\"Price\" IS NULL OR \"Price\" >= 0");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitTypes_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    AlertTypeId = table.Column<int>(type: "integer", nullable: false),
                    AlertStatusId = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    AcknowledgedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryAlerts_AlertStatus_AlertStatusId",
                        column: x => x.AlertStatusId,
                        principalTable: "AlertStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryAlerts_AlertTypes_AlertTypeId",
                        column: x => x.AlertTypeId,
                        principalTable: "AlertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryAlerts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AlertStatus",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "#f59e0b", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "La alerta está activa y requiere atención", true, "Activa" },
                    { 2, "#3b82f6", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "La alerta ha sido vista y reconocida por el usuario", true, "Reconocida" }
                });

            migrationBuilder.InsertData(
                table: "AlertStatus",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "IsActive", "IsFinalState", "Name" },
                values: new object[,]
                {
                    { 3, "#22c55e", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "La alerta ha sido resuelta y está cerrada", true, true, "Resuelta" },
                    { 4, "#6b7280", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "La alerta ha sido descartada sin acción", true, true, "Descartada" }
                });

            migrationBuilder.InsertData(
                table: "AlertStatus",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[] { 5, "#8b5cf6", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "La alerta está siendo atendida actualmente", true, "En Proceso" });

            migrationBuilder.InsertData(
                table: "AlertTypes",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "IsActive", "Name", "Priority" },
                values: new object[,]
                {
                    { 1, "#f59e0b", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "El stock del producto está por debajo del mínimo establecido", true, "Stock Bajo", "Medio" },
                    { 2, "#ef4444", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "El producto está completamente agotado", true, "Sin Stock", "Alto" },
                    { 3, "#f97316", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "El producto está cerca de su fecha de vencimiento", true, "Próximo a Vencer", "Bajo" },
                    { 4, "#8b5cf6", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "El producto tiene stock excesivamente alto", true, "Exceso de Stock", "Bajo" },
                    { 5, "#06b6d4", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "El precio del producto ha sido modificado", true, "Precio Cambiado", "Informativo" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "#22c55e", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos alimenticios y comestibles", true, "Alimentos" },
                    { 2, "#06b6d4", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Bebidas y refrescos", true, "Bebidas" },
                    { 3, "#3b82f6", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos de limpieza para el hogar", true, "Limpieza" },
                    { 4, "#f59e0b", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos de higiene y cuidado personal", true, "Higiene Personal" },
                    { 5, "#ef4444", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Medicamentos y productos farmacéuticos", true, "Medicinas" },
                    { 6, "#8b5cf6", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Artículos para el hogar y decoración", true, "Hogar" },
                    { 7, "#10b981", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos para mascotas y animales", true, "Mascotas" },
                    { 8, "#6b7280", new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos varios sin categoría específica", true, "Otros" }
                });

            migrationBuilder.InsertData(
                table: "UnitTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos por unidad individual", true, "Unidades", "un" },
                    { 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Peso en kilogramos", true, "Kilogramos", "kg" },
                    { 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Peso en gramos", true, "Gramos", "g" },
                    { 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Volumen en litros", true, "Litros", "L" },
                    { 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Volumen en mililitros", true, "Mililitros", "mL" },
                    { 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en paquetes", true, "Paquetes", "paq" },
                    { 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en cajas", true, "Cajas", "cajas" },
                    { 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en botellas", true, "Botellas", "bot" },
                    { 9, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en tubos", true, "Tubos", "tubos" },
                    { 10, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos por docenas", true, "Docenas", "doc" },
                    { 11, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en latas", true, "Latas", "latas" },
                    { 12, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Productos en sobres", true, "Sobres", "sobres" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertStatuses_Name",
                table: "AlertStatus",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AlertTypes_Name",
                table: "AlertTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAlerts_AlertStatusId",
                table: "InventoryAlerts",
                column: "AlertStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAlerts_AlertTypeId",
                table: "InventoryAlerts",
                column: "AlertTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAlerts_CreatedAt",
                table: "InventoryAlerts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAlerts_ProductId",
                table: "InventoryAlerts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAlerts_Status_Type",
                table: "InventoryAlerts",
                columns: new[] { "AlertStatusId", "AlertTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive",
                table: "Products",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive_CategoryId",
                table: "Products",
                columns: new[] { "IsActive", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTypes_IsActive",
                table: "UnitTypes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTypes_Name",
                table: "UnitTypes",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryAlerts");

            migrationBuilder.DropTable(
                name: "AlertStatus");

            migrationBuilder.DropTable(
                name: "AlertTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UnitTypes");
        }
    }
}
