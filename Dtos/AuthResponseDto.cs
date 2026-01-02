namespace PgCrudApi.Dtos;

public record AuthResponseDto(
    string Token,
    string Username,
    string Email
);
