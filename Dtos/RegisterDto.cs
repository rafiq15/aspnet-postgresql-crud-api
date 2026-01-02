using System.ComponentModel.DataAnnotations;

namespace PgCrudApi.Dtos;

public record RegisterDto(
    [Required][MaxLength(100)] string Username,
    [Required][EmailAddress][MaxLength(200)] string Email,
    [Required][MinLength(6)] string Password
);
