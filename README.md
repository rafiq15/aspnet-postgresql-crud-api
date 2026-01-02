# PgCrudApi - Product & User Management System

ASP.NET Core 9.0 Web API with JWT authentication, PostgreSQL database, and complete CRUD operations for products and user management. This project demonstrates modern API development with security, database migrations, and interactive API documentation using Swagger.

## Features

### Authentication & Authorization
- ✅ JWT (JSON Web Token) authentication
- ✅ User registration and login
- ✅ Secure password hashing (SHA-256)
- ✅ Protected endpoints with `[Authorize]` attribute
- ✅ Token-based authentication in Swagger UI

### Product Management
- ✅ Full CRUD operations (Create, Read, Update, Delete)
- ✅ RESTful API design
- ✅ Protected endpoints requiring authentication
- ✅ DTO pattern for data transfer
- ✅ Entity mapping with custom mappers

### User Management
- ✅ User registration with email and username validation
- ✅ User login with JWT token generation
- ✅ Get all users (authenticated)
- ✅ Get user by ID (authenticated)
- ✅ Update user profile (authenticated)
- ✅ Delete user (authenticated)
- ✅ Duplicate email/username prevention

### Technical Features
- ✅ PostgreSQL database with Entity Framework Core
- ✅ Async/await operations for better performance
- ✅ Database migrations with EF Core
- ✅ Swagger UI with JWT authentication support
- ✅ CORS enabled for frontend integration
- ✅ Custom Swagger operation filter for security
- ✅ Proper error handling and validation

## Tech Stack

- **Framework:** ASP.NET Core 9.0
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core 9.0
- **Authentication:** JWT Bearer Token
- **Language:** C# 12
- **API Documentation:** Swagger/OpenAPI (Swashbuckle 7.2.0)

### NuGet Packages
- `Microsoft.AspNetCore.Authentication.JwtBearer` (9.0.0)
- `Microsoft.AspNetCore.OpenApi` (9.0.10)
- `Microsoft.EntityFrameworkCore.Design` (9.0.11)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (9.0.4)
- `Swashbuckle.AspNetCore` (7.2.0)
- `System.IdentityModel.Tokens.Jwt` (8.15.0)

## Prerequisites

Before running this project, ensure you have:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- A code editor (Visual Studio 2022, VS Code, or Rider)

## Database Setup

### 1. Configure Database Connection

Update the connection string in `appsettings.json` if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pgcruddb;Username=postgres;Password=root"
  }
}
```

### 2. Configure JWT Settings

The JWT configuration is also in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyForJWTAuthenticationMinimum32Characters!",
    "Issuer": "PgCrudApi",
    "Audience": "PgCrudApiUsers",
    "ExpireMinutes": 60
  }
}
```

**Important:** Change the `Key` value to your own secret key in production!

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

This will create the database and apply all migrations including:
- `InitialCreate` - Creates the Products table
- `AddUserTable` - Creates the Users table

## Running the Application

### Start the API

```bash
dotnet run
```

The API will be available at:
- **HTTP:** `http://localhost:5163`
- **Swagger UI:** `http://localhost:5163/swagger/index.html`

## Using Swagger UI

1. Navigate to `http://localhost:5163/swagger/index.html`
2. Test the authentication endpoints first (register/login)
3. Copy the JWT token from the response
4. Click the **Authorize** button at the top right
5. Enter: `Bearer YOUR_JWT_TOKEN` (replace YOUR_JWT_TOKEN with the actual token)
6. Click **Authorize**
7. Now you can test all protected endpoints!

## API Endpoints

### Authentication Endpoints (Public)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register a new user | No |
| POST | `/api/auth/login` | Login and get JWT token | No |

### User Management Endpoints (Protected)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/auth/users` | Get all users | Yes |
| GET | `/api/auth/users/{id}` | Get user by ID | Yes |
| PUT | `/api/auth/users/{id}` | Update user profile | Yes |
| DELETE | `/api/auth/users/{id}` | Delete user | Yes |

### Product Endpoints (Protected)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/products` | Get all products | Yes |
| GET | `/api/products/{id}` | Get a product by ID | Yes |
| POST | `/api/products` | Create a new product | Yes |
| PUT | `/api/products/{id}` | Update an existing product | Yes |
| DELETE | `/api/products/{id}` | Delete a product | Yes |

## API Usage Examples

### 1. Register a New User

**Request:**
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "johndoe",
  "email": "john.doe@example.com",
  "password": "SecurePassword123"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "johndoe",
  "email": "john.doe@example.com"
}
```

### 2. Login

**Request:**
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john.doe@example.com",
  "password": "SecurePassword123"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "johndoe",
  "email": "john.doe@example.com"
}
```

### 3. Get All Users (Protected)

**Request:**
```http
GET /api/auth/users
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "username": "johndoe",
    "email": "john.doe@example.com",
    "createdAt": "2026-01-01T10:30:00Z"
  }
]
```

### 4. Update User

**Request:**
```http
PUT /api/auth/users/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "username": "john_updated",
  "email": "john.updated@example.com",
  "password": "NewPassword456"
}
```

**Response (200 OK):**
```json
{
  "id": 1,
  "username": "john_updated",
  "email": "john.updated@example.com",
  "createdAt": "2026-01-01T10:30:00Z"
}
```

### 5. Create Product (Protected)

**Request:**
```http
POST /api/products
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "createdAt": "2026-01-02T10:30:00Z"
}
```

### 6. Get All Products (Protected)

**Request:**
```http
GET /api/products
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 999.99,
    "createdAt": "2026-01-02T10:30:00Z"
  },
  {
    "id": 2,
    "name": "Mouse",
    "description": "Wireless mouse",
    "price": 29.99,
    "createdAt": "2026-01-02T11:00:00Z"
  }
]
```

### 7. Update Product (Protected)

**Request:**
```http
PUT /api/products/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "name": "Gaming Laptop",
  "description": "High-performance gaming laptop with RTX 4090",
  "price": 1299.99
}
```

**Response:**
```
204 No Content
```

### 8. Delete Product (Protected)

**Request:**
```http
DELETE /api/products/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:**
```
204 No Content
```

## Project Structure

```
PgCrudApi/
├── Controllers/
│   ├── AuthController.cs         # Authentication & user management endpoints
│   └── ProductsController.cs     # Product CRUD endpoints
├── Data/
│   ├── ProductDbContext.cs       # Database context
│   └── Migrations/                # EF Core migrations
│       ├── InitialCreate.cs      # Initial Products table
│       └── AddUserTable.cs       # User authentication table
├── Dtos/
│   ├── AuthResponseDto.cs        # JWT token response
│   ├── LoginDto.cs               # Login request
│   ├── RegisterDto.cs            # User registration request
│   ├── UserDto.cs                # User response
│   ├── UpdateUserDto.cs          # Update user request
│   ├── CreateProductDto.cs       # Create product request
│   ├── ProductDto.cs             # Product response
│   └── UpdateProductDto.cs       # Update product request
├── mapper/
│   ├── ProductMapping.cs         # Product entity-DTO mappings
│   └── UserMapping.cs            # User entity-DTO mappings
├── Models/
│   ├── Product.cs                # Product entity
│   └── User.cs                   # User entity
├── Properties/
│   └── launchSettings.json       # Launch configuration
├── SwaggerAuthOperationFilter.cs # Swagger JWT authentication filter
├── appsettings.json              # App configuration (connection strings, JWT)
├── Program.cs                    # Application entry point & middleware setup
└── PgCrudApi.csproj              # Project file & NuGet packages
```

## Database Schema

### Users Table

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | int | PRIMARY KEY, AUTO_INCREMENT | Unique user identifier |
| Username | string(100) | REQUIRED, UNIQUE | User's username |
| Email | string(200) | REQUIRED, UNIQUE | User's email address |
| PasswordHash | string | REQUIRED | Hashed password (SHA-256) |
| CreatedAt | datetime | DEFAULT UTC NOW | Account creation timestamp |

### Products Table

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | int | PRIMARY KEY, AUTO_INCREMENT | Unique product identifier |
| Name | string | REQUIRED | Product name |
| Description | string | REQUIRED | Product description |
| Price | decimal | REQUIRED | Product price |
| CreatedAt | datetime | DEFAULT UTC NOW | Creation timestamp |

## Security Features

- **JWT Authentication**: Secure token-based authentication
- **Password Hashing**: SHA-256 hashing for password security
- **Protected Endpoints**: All product and user management endpoints require authentication
- **Token Expiration**: Tokens expire after 60 minutes (configurable)
- **Duplicate Prevention**: Email and username uniqueness validation
- **CORS Configuration**: Configured for cross-origin requests

## Development

### Adding New Migrations

When you modify the entity models, create a new migration:

```bash
# Create a new migration
dotnet ef migrations add YourMigrationName

# Apply the migration to database
dotnet ef database update
```

### Remove Last Migration

```bash
dotnet ef migrations remove
```

### View Migration SQL

```bash
dotnet ef migrations script
```

## Common Issues & Solutions

### 1. Database Connection Errors
**Issue:** Cannot connect to PostgreSQL database

**Solution:**
- Ensure PostgreSQL service is running
- Verify connection string credentials in `appsettings.json`
- Check PostgreSQL port (default: 5432)
- Verify database exists or run migrations to create it

### 2. JWT Authentication Errors
**Issue:** 401 Unauthorized when calling protected endpoints

**Solution:**
- Ensure you have registered/logged in and received a JWT token
- Include the token in the Authorization header: `Bearer YOUR_TOKEN`
- Check token expiration (default: 60 minutes)
- Verify JWT configuration in `appsettings.json`

### 3. Migration Errors
**Issue:** Migration failed or database out of sync

**Solution:**
- Ensure EF Core tools are installed: `dotnet tool install --global dotnet-ef`
- Delete the database and run migrations again
- Check for conflicting migrations
- Verify connection string is correct

### 4. CORS Errors
**Issue:** CORS policy blocking requests

**Solution:**
- Verify CORS policy in `Program.cs`
- Ensure your frontend URL is in the allowed origins
- Check that CORS middleware is properly configured

### 5. Swagger Not Loading
**Issue:** Swagger UI not accessible

**Solution:**
- Ensure application is running in Development mode
- Check `launchSettings.json` for environment settings
- Navigate to correct URL: `http://localhost:5163/swagger/index.html`

## Testing with Swagger

1. **Start the application**: `dotnet run`
2. **Open Swagger UI**: Navigate to `http://localhost:5163/swagger/index.html`
3. **Register a user**:
   - Expand `POST /api/auth/register`
   - Click "Try it out"
   - Fill in the request body
   - Click "Execute"
   - Copy the `token` from the response
4. **Authorize**:
   - Click the "Authorize" button (lock icon) at the top right
   - Enter: `Bearer YOUR_TOKEN_HERE`
   - Click "Authorize" then "Close"
5. **Test protected endpoints**: Now you can test all product and user endpoints!

## Environment Variables

For production, consider using environment variables instead of storing sensitive data in `appsettings.json`:

```bash
# Windows PowerShell
$env:ConnectionStrings__DefaultConnection = "Host=your-host;Port=5432;Database=pgcruddb;Username=user;Password=pass"
$env:Jwt__Key = "YourProductionSecretKey"

# Linux/Mac
export ConnectionStrings__DefaultConnection="Host=your-host;Port=5432;Database=pgcruddb;Username=user;Password=pass"
export Jwt__Key="YourProductionSecretKey"
```

## API Response Codes

| Status Code | Description |
|-------------|-------------|
| 200 OK | Request successful |
| 201 Created | Resource created successfully |
| 204 No Content | Request successful (no response body) |
| 400 Bad Request | Invalid request data or validation error |
| 401 Unauthorized | Missing or invalid JWT token |
| 404 Not Found | Resource not found |
| 500 Internal Server Error | Server error |

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is open source and available for educational purposes.

## Author

https://rafiq15.github.io/

## Acknowledgments

- ASP.NET Core documentation
- Entity Framework Core documentation
- PostgreSQL community

---

**Note:** This is a demonstration project. For production use, consider implementing:
- More secure password hashing (e.g., BCrypt, Argon2)
- Refresh tokens for better security
- Role-based authorization (Admin, User roles)
- Input validation and sanitization
- Rate limiting to prevent abuse
- Logging and monitoring
- Comprehensive error handling
- Unit and integration tests
- API versioning
- Health check endpoints
