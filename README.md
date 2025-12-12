# ASP.NET PostgreSQL CRUD API

A clean and modern REST API built with ASP.NET Core 9.0 and PostgreSQL, demonstrating complete CRUD operations for product management. This project implements best practices including DTOs, entity mapping, async operations, and comprehensive API documentation with Swagger.

## Features

- ✅ Full CRUD operations (Create, Read, Update, Delete)
- ✅ RESTful API design
- ✅ PostgreSQL database with Entity Framework Core
- ✅ DTO pattern for data transfer
- ✅ Async/await operations for better performance
- ✅ Database migrations with EF Core
- ✅ Swagger UI for API documentation and testing
- ✅ Clean architecture with separation of concerns

## Tech Stack

- **Framework:** ASP.NET Core 9.0
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Language:** C# 12
- **API Documentation:** Swagger/OpenAPI

## Prerequisites

Before running this project, ensure you have:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- A code editor (Visual Studio 2022, VS Code, or Rider)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/aspnet-postgresql-crud-api.git
cd aspnet-postgresql-crud-api
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=productdb;Username=your_username;Password=your_password"
  }
}
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get a product by ID |
| POST | `/api/products` | Create a new product |
| PUT | `/api/products/{id}` | Update an existing product |
| DELETE | `/api/products/{id}` | Delete a product |

## Request/Response Examples

### Create Product (POST)

**Request:**
```json
POST /api/products
Content-Type: application/json

{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "createdAt": "2025-12-12T10:30:00Z"
}
```

### Get All Products (GET)

**Request:**
```
GET /api/products
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 999.99,
    "createdAt": "2025-12-12T10:30:00Z"
  },
  {
    "id": 2,
    "name": "Mouse",
    "description": "Wireless mouse",
    "price": 29.99,
    "createdAt": "2025-12-12T11:00:00Z"
  }
]
```

### Update Product (PUT)

**Request:**
```json
PUT /api/products/1
Content-Type: application/json

{
  "name": "Gaming Laptop",
  "description": "High-performance gaming laptop",
  "price": 1299.99
}
```

**Response:**
```
204 No Content
```

### Delete Product (DELETE)

**Request:**
```
DELETE /api/products/1
```

**Response:**
```
204 No Content
```

## Project Structure

```
PgCrudApi/
├── Controllers/
│   └── ProductsController.cs    # API endpoints
├── Data/
│   ├── ProductDbContext.cs      # Database context
│   └── Migrations/               # EF Core migrations
├── Dtos/
│   ├── CreateProductDto.cs      # DTO for creating products
│   ├── ProductDto.cs            # DTO for product responses
│   └── UpdateProductDto.cs      # DTO for updating products
├── mapper/
│   └── ProductMapping.cs        # Entity-DTO mappings
├── Models/
│   └── Product.cs               # Product entity
├── Properties/
│   └── launchSettings.json      # Launch configuration
├── appsettings.json             # App configuration
├── Program.cs                   # Application entry point
└── PgCrudApi.csproj             # Project file
```

## Database Schema

### Products Table

| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key (auto-increment) |
| Name | string | Product name |
| Description | string | Product description |
| Price | decimal | Product price |
| CreatedAt | datetime | Creation timestamp |

## Development

### Adding New Migrations

When you modify the entity models, create a new migration:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Running Tests

```bash
dotnet test
```

## Common Issues

### Connection Issues
- Ensure PostgreSQL is running
- Verify connection string credentials
- Check firewall settings

### Migration Errors
- Delete the database and run migrations again
- Ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Your Name - [@yourhandle](https://github.com/yourusername)

## Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core documentation
- PostgreSQL community
