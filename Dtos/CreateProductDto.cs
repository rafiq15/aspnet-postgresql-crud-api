using System.ComponentModel.DataAnnotations;

namespace PgCrudApi.Dtos;

public record class CreateProductDto(
    [Required] [StringLength(100)] string Name,
    [Required] [StringLength(500)] string Description,
    [Range(0, 500)] decimal Price
);