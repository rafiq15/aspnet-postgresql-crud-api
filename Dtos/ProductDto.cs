namespace PgCrudApi.Dtos;

public record class ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    DateTime CreatedAt
);