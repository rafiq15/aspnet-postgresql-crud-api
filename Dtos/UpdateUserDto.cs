using System.ComponentModel.DataAnnotations;

namespace PgCrudApi.Dtos;

public record class UpdateUserDto
{
    [MaxLength(100)]
    public string? Username { get; set; }
    
    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; }
    
    [MinLength(6)]
    public string? Password { get; set; }
}
