namespace PgCrudApi.Dtos;

public record class UpdateProductDto(
    string? Name,
    string? Description,
    decimal? Price
);
