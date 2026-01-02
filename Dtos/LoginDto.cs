using System.ComponentModel.DataAnnotations;

namespace PgCrudApi.Dtos;

public record LoginDto(
    [Required][EmailAddress] string Email,
    [Required] string Password
);
