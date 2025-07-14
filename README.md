# 🚀 RestockAPI - Inventory Management Backend

A robust RESTful API built with .NET 8 and Entity Framework Core, providing comprehensive inventory management capabilities with PostgreSQL database integration.

## 🏗️ Architecture Overview

RestockAPI follows a clean architecture pattern with clear separation of concerns:

```
RestockAPI/
├── Controllers/          # API endpoints and HTTP request handling
├── Services/            # Business logic and application services
├── DTOs/               # Data Transfer Objects for API communication
├── Models/             # Domain entities and database models
├── Data/               # Database context and configurations
├── Migrations/         # Database schema migrations
└── Properties/         # Application configuration
```

## 🛠️ Technology Stack

- **.NET 8** - Latest LTS framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core 9.0** - Object-Relational Mapping
- **PostgreSQL** - Primary database
- **Npgsql** - PostgreSQL .NET provider
- **Swagger/OpenAPI** - API documentation
- **Docker** - Database containerization

## ✨ Features

### 🎯 **Core Business Logic**

- **Product Management** - CRUD operations with stock tracking
- **Category Organization** - Hierarchical product categorization
- **Inventory Alerts** - Automated low stock notifications
- **Unit Type Management** - Flexible measurement units
- **Stock Operations** - Real-time inventory updates

### 🔧 **Technical Features**

- **RESTful Design** - Standard HTTP methods and status codes
- **Async/Await Pattern** - Non-blocking operations
- **Dependency Injection** - Loosely coupled architecture
- **CORS Configuration** - Cross-origin request handling
- **Error Handling** - Comprehensive exception management
- **Logging** - Structured application logging

## 🚦 API Endpoints

### **Products**

```http
GET    /api/product/getProducts                    # Get all products
GET    /api/product/getActiveProducts              # Get active products only
GET    /api/product/getProductById/{id}            # Get product by ID
GET    /api/product/getCantProducts                # Get product count
GET    /api/product/getCantLowStockProducts        # Get low stock count
POST   /api/product/createProduct                  # Create new product
PATCH  /api/product/updateProduct/{id}             # Update product
PATCH  /api/product/updateStock/{id}               # Update stock level
PATCH  /api/product/toggleProductActive/{id}       # Toggle active status
```

### **Categories**

```http
GET    /api/category/getCategories                 # Get all categories
GET    /api/category/getCategoryById/{id}          # Get category by ID
GET    /api/category/getCantCategories             # Get category count
POST   /api/category/createCategory                # Create new category
PATCH  /api/category/updateCategory/{id}           # Update category
DELETE /api/category/deleteCategory/{id}           # Soft delete category
```

### **Inventory Alerts**

```http
GET    /api/alert/getInventoryAlerts               # Get all alerts
GET    /api/alert/getAlertById/{id}                # Get alert by ID
GET    /api/alert/getAlertsByProduct/{productId}   # Get alerts by product
POST   /api/alert/createAlert                      # Create new alert
PATCH  /api/alert/updateAlert/{id}                 # Update alert
DELETE /api/alert/deleteAlert/{id}                 # Delete alert
```

### **Unit Types**

```http
GET    /api/unit/getUnitTypes                      # Get all unit types
GET    /api/unit/getUnitTypeById/{id}              # Get unit type by ID
```

## 📊 Data Models

### **Core Entities**

#### **Product**

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal? Price { get; set; }
    public int CategoryId { get; set; }
    public int UnitId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ImageUrl { get; set; }

    // Navigation properties
    public virtual Category Category { get; set; }
    public virtual UnityType Unit { get; set; }
}
```

#### **Category**

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Color { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; }
}
```

#### **InventoryAlert**

```csharp
public class InventoryAlert
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AlertTypeId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AlertStatusId { get; set; }

    // Navigation properties
    public virtual Product Product { get; set; }
    public virtual AlertType AlertType { get; set; }
    public virtual AlertStatus AlertStatus { get; set; }
}
```

## 🗃️ Database Schema

### **Tables Structure**

- **Products** - Core inventory items
- **Categories** - Product organization
- **UnitTypes** - Measurement units (kg, pieces, liters, etc.)
- **InventoryAlerts** - System notifications
- **AlertTypes** - Alert classification
- **AlertStatus** - Alert state management

### **Key Relationships**

- Product → Category (Many-to-One)
- Product → UnitType (Many-to-One)
- InventoryAlert → Product (Many-to-One)
- InventoryAlert → AlertType (Many-to-One)

## 🚀 Getting Started

### **Prerequisites**

- .NET 8 SDK
- Docker & Docker Compose
- PostgreSQL (via Docker)

### **1. Clone and Setup**

```bash
# Clone the repository
git clone <repository-url>
cd RestockAPI

# Restore NuGet packages
dotnet restore
```

### **2. Database Setup**

```bash
# Start PostgreSQL container
docker-compose up -d

# Apply database migrations
dotnet ef database update

# Verify database connection
dotnet ef database list
```

### **3. Run the API**

```bash
# Development mode
dotnet run

# Watch mode (auto-reload)
dotnet watch run

# Production mode
dotnet run --environment Production
```

API will be available at:

- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger UI**: `http://localhost:5000/swagger`

## ⚙️ Configuration

### **appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=restock_db;Username=root;Password=root"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### **Environment Variables**

```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://localhost:5000
DATABASE_URL=postgresql://root:root@localhost:5432/restock_db
```

## 🧪 Development

### **Entity Framework Commands**

```bash
# Create new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Generate SQL scripts
dotnet ef script
```

### **Build & Test**

```bash
# Build solution
dotnet build

# Run tests
dotnet test

# Publish for deployment
dotnet publish -c Release
```

### **Docker Development**

```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop services
docker-compose down

# Rebuild containers
docker-compose up -d --build
```

## 📋 Service Layer

### **IProductService**

```csharp
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> UpdateStockAsync(int id, UpdateStockDto updateStockDto);
    Task<bool> ProductExistsAsync(int id);
}
```

### **ICategoryService**

```csharp
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}
```

## 🔍 API Documentation

### **Swagger Integration**

Access interactive API documentation at `/swagger` when running in development mode.

### **Response Formats**

#### **Success Response**

```json
{
  "id": 1,
  "name": "Product Name",
  "description": "Product description",
  "currentStock": 100,
  "minimumStock": 10,
  "price": 29.99,
  "categoryId": 1,
  "unitId": 1,
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

#### **Error Response**

```json
{
  "message": "Product with ID 999 not found",
  "timestamp": "2024-01-01T00:00:00Z",
  "path": "/api/product/getProductById/999"
}
```

## 🔐 Security Considerations

- **CORS Configuration** - Proper origin handling
- **Input Validation** - DTO validation attributes
- **SQL Injection Prevention** - Entity Framework parameterization
- **Error Handling** - Secure error messages

## 📈 Performance Optimizations

- **Async/Await** - Non-blocking operations
- **Entity Framework Tracking** - Optimized queries
- **Connection Pooling** - Database connection management
- **Caching Strategy** - Ready for implementation

## 🧪 Testing

### **Test Structure**

```bash
# Unit tests
dotnet test --filter Category=Unit

# Integration tests
dotnet test --filter Category=Integration

# Coverage report
dotnet test --collect:"XPlat Code Coverage"
```

## 📦 Deployment

### **Production Checklist**

- [ ] Update connection strings
- [ ] Configure environment variables
- [ ] Enable HTTPS redirection
- [ ] Set up logging providers
- [ ] Configure health checks
- [ ] Implement monitoring

### **Docker Production**

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "RestockAPI.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Implement changes with tests
4. Ensure migrations are included
5. Submit a pull request

### **Code Standards**

- Follow C# naming conventions
- Document public APIs with XML comments
- Include unit tests for business logic
- Update database migrations when needed

---

**Built with .NET 8 for scalable inventory management** 🚀
