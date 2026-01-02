using System;
using PgCrudApi.Dtos;
using PgCrudApi.Models;

namespace PgCrudApi.mapper;

public static class UserMapping
{
    public static User ToEntity(this RegisterDto dto)
    {
        return new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password, // In real scenarios, hash the password before storing
            CreatedAt = DateTime.UtcNow
        };
    }
    public static User ToEntity(this UpdateUserDto dto, User existingUser, string? passwordHash = null)
    {
        if (!string.IsNullOrWhiteSpace(dto.Username))
            existingUser.Username = dto.Username;
        
        if (!string.IsNullOrWhiteSpace(dto.Email))
            existingUser.Email = dto.Email;
        
        if (!string.IsNullOrWhiteSpace(passwordHash))
            existingUser.PasswordHash = passwordHash;

        return existingUser;
    }

    public static UserDto ToDto(this User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt
        };
    }

}
