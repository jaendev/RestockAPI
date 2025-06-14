using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestockAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsAndAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 25m, "Arroz blanco de grano largo 1kg", null, true, 10m, "Arroz Blanco", 3.50m, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Aceite de oliva virgen extra 500ml", null, true, 5m, "Aceite de Oliva", 8.90m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 2m, "Pan de molde integral 500g", null, true, 5m, "Pan de Molde", 1.80m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 4, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Huevos frescos de granja", null, true, 3m, "Huevos Frescos", 4.20m, 10, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 5, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 15m, "Leche entera pasteurizada 1L", null, true, 8m, "Leche Entera", 2.50m, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1m, "Azúcar refinada 1kg", null, true, 4m, "Azúcar Blanca", 2.20m, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 30m, "Agua mineral natural 1.5L", null, true, 12m, "Agua Mineral", 1.20m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 5m, "Refresco de cola 2L", null, true, 6m, "Coca Cola", 3.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Jugo natural de naranja 1L", null, true, 4m, "Jugo de Naranja", 2.80m, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 10, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Cerveza artesanal 330ml", null, true, 6m, "Cerveza Artesanal", 2.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 11, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 12m, "Café molido natural 500g", null, true, 5m, "Café Molido", 7.50m, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 6m, "Detergente para ropa 2L", null, true, 3m, "Detergente Líquido", 12.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1m, "Papel higiénico suave 12 rollos", null, true, 2m, "Papel Higiénico", 8.90m, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 4m, "Spray limpiavidrios 500ml", null, true, 2m, "Limpiavidrios", 4.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 15, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Lejía desinfectante 1L", null, true, 3m, "Lejía", 2.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 16, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Esponjas de cocina pack x6", null, true, 4m, "Esponjas Cocina", 3.20m, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 5m, "Champú para cabello normal 400ml", null, true, 3m, "Champú", 6.80m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 2m, "Pasta dental con flúor 100ml", null, true, 4m, "Pasta Dental", 3.20m, 9, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 7m, "Jabón líquido antibacterial 250ml", null, true, 3m, "Jabón de Manos", 4.90m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 20, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Desodorante en aerosol 150ml", null, true, 2m, "Desodorante", 5.50m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 21, 4, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 9m, "Gel de ducha hidratante 500ml", null, true, 4m, "Gel de Ducha", 6.20m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 45m, "Analgésico y antifebril", null, true, 20m, "Paracetamol 500mg", 0.15m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 3m, "Alcohol etílico desinfectante 250ml", null, true, 5m, "Alcohol 70%", 2.50m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Vendas elásticas 5cm x 4.5m", null, true, 4m, "Vendas Elásticas", 3.80m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 25, 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Antiinflamatorio", null, true, 15m, "Ibuprofeno 400mg", 0.20m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 26, 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 25m, "Suplemento vitamina C 1000mg", null, true, 10m, "Vitamina C", 0.25m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Bombilla LED 10W luz cálida", null, true, 5m, "Bombillas LED", 4.50m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 2m, "Pilas alcalinas AA pack x4", null, true, 6m, "Pilas AA", 6.80m, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 12m, "Velas aromáticas de lavanda", null, true, 4m, "Velas Aromáticas", 8.90m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 30, 6, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Rollo papel aluminio 25m", null, true, 3m, "Papel Aluminio", 3.20m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 31, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 5m, "Alimento balanceado para perros 3kg", null, true, 3m, "Comida para Perros", 15.50m, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1m, "Arena sanitaria para gatos 5kg", null, true, 4m, "Arena para Gatos", 8.90m, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 8m, "Pelota de goma resistente", null, true, 3m, "Juguete para Perros", 6.50m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 34, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Shampoo neutro para perros y gatos 500ml", null, true, 2m, "Shampoo para Mascotas", 12.80m, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CurrentStock", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[,]
                {
                    { 35, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 4m, "Cargador universal USB tipo C", null, true, 2m, "Cargador USB", 12.50m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 1m, "Paraguas compacto resistente al viento", null, true, 3m, "Paraguas", 18.90m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), 6m, "Linterna LED recargable", null, true, 2m, "Linterna LED", 15.20m, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "MinimumStock", "Name", "Price", "UnitId", "UpdatedAt" },
                values: new object[] { 38, 8, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Mascarillas desechables caja x50", null, true, 5m, "Mascarillas", 8.50m, 7, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "InventoryAlerts",
                columns: new[] { "Id", "AcknowledgedAt", "AlertStatusId", "AlertTypeId", "CreatedAt", "Message", "ProductId", "ResolvedAt" },
                values: new object[,]
                {
                    { 1, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Pan de Molde: Stock actual (2) por debajo del mínimo (5)", 3, null },
                    { 2, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Azúcar Blanca: Stock actual (1) por debajo del mínimo (4)", 6, null },
                    { 3, new DateTime(2025, 6, 14, 11, 0, 0, 0, DateTimeKind.Utc), 2, 1, new DateTime(2025, 6, 14, 10, 0, 0, 0, DateTimeKind.Utc), "Coca Cola: Stock actual (5) por debajo del mínimo (6)", 8, null },
                    { 4, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Papel Higiénico: Stock actual (1) por debajo del mínimo (2)", 13, null },
                    { 5, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Pasta Dental: Stock actual (2) por debajo del mínimo (4)", 18, null },
                    { 6, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Alcohol 70%: Stock actual (3) por debajo del mínimo (5)", 23, null },
                    { 7, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Pilas AA: Stock actual (2) por debajo del mínimo (6)", 28, null },
                    { 8, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Arena para Gatos: Stock actual (1) por debajo del mínimo (4)", 32, null },
                    { 9, null, 1, 1, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Paraguas: Stock actual (1) por debajo del mínimo (3)", 36, null },
                    { 10, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Huevos Frescos: Producto completamente agotado", 4, null },
                    { 11, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Cerveza Artesanal: Producto completamente agotado", 10, null },
                    { 12, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Lejía: Producto completamente agotado", 15, null },
                    { 13, new DateTime(2025, 6, 14, 10, 0, 0, 0, DateTimeKind.Utc), 2, 2, new DateTime(2025, 6, 14, 9, 0, 0, 0, DateTimeKind.Utc), "Desodorante: Producto completamente agotado", 20, null },
                    { 14, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Ibuprofeno 400mg: Producto completamente agotado", 25, null },
                    { 15, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Papel Aluminio: Producto completamente agotado", 30, null },
                    { 16, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Shampoo para Mascotas: Producto completamente agotado", 34, null },
                    { 17, null, 1, 2, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Mascarillas: Producto completamente agotado", 38, null },
                    { 18, new DateTime(2025, 6, 13, 12, 0, 0, 0, DateTimeKind.Utc), 3, 3, new DateTime(2025, 6, 12, 12, 0, 0, 0, DateTimeKind.Utc), "Leche Entera: Próxima a vencer en 3 días", 5, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, null, 1, 3, new DateTime(2025, 6, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Jugo de Naranja: Próximo a vencer en 2 días", 9, null },
                    { 20, new DateTime(2025, 6, 14, 9, 0, 0, 0, DateTimeKind.Utc), 2, 3, new DateTime(2025, 6, 14, 8, 0, 0, 0, DateTimeKind.Utc), "Paracetamol 500mg: Próximo a vencer en 30 días", 22, null },
                    { 21, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 4, 4, new DateTime(2025, 6, 13, 12, 0, 0, 0, DateTimeKind.Utc), "Agua Mineral: Stock excesivo detectado (30 unidades)", 7, null },
                    { 22, new DateTime(2025, 6, 14, 7, 0, 0, 0, DateTimeKind.Utc), 3, 5, new DateTime(2025, 6, 14, 6, 0, 0, 0, DateTimeKind.Utc), "Café Molido: Precio actualizado de €6.80 a €7.50", 11, new DateTime(2025, 6, 14, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, new DateTime(2025, 6, 14, 11, 30, 0, 0, DateTimeKind.Utc), 2, 5, new DateTime(2025, 6, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Bombillas LED: Precio actualizado de €3.90 a €4.50", 27, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "InventoryAlerts",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);
        }
    }
}
