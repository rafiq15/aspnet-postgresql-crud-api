using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PgCrudApi.Data;
using PgCrudApi.Dtos;
using PgCrudApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using PgCrudApi.mapper;

namespace PgCrudApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ProductDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ProductDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // POST api/auth/register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
    {
        // Check if user already exists
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return BadRequest("User with this email already exists");
        }

        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
        {
            return BadRequest("Username is already taken");
        }

        // Hash password
        var passwordHash = HashPassword(registerDto.Password);

        // Create new user  

        var entity = registerDto.ToEntity();
        entity.PasswordHash = passwordHash;

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = GenerateJwtToken(entity);
        return Ok(new AuthResponseDto(token, entity.Username, entity.Email));
    }

    // POST api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        // Find user by email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        // Verify password
        if (!VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password");
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);

        return Ok(new AuthResponseDto(token, user.Username, user.Email));
    }

    // GET api/auth/users
    [HttpGet("users")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users.Select(u => u.ToDto()).ToList());
    }

    // GET api/auth/users/{id}
    [HttpGet("users/{id}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        return Ok(user.ToDto());
    }

    // PUT api/auth/users/{id}
    [HttpPut("users/{id}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
            return NotFound("User not found");

        // Check if email is being changed and is already taken by another user
        if (!string.IsNullOrWhiteSpace(updateUserDto.Email) &&
            updateUserDto.Email != existingUser.Email &&
            await _context.Users.AnyAsync(u => u.Email == updateUserDto.Email && u.Id != id))
        {
            return BadRequest("Email is already taken by another user");
        }

        // Check if username is being changed and is already taken by another user
        if (!string.IsNullOrWhiteSpace(updateUserDto.Username) &&
            updateUserDto.Username != existingUser.Username &&
            await _context.Users.AnyAsync(u => u.Username == updateUserDto.Username && u.Id != id))
        {
            return BadRequest("Username is already taken by another user");
        }

        // Hash new password if provided
        string? passwordHash = null;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
        {
            passwordHash = HashPassword(updateUserDto.Password);
        }

        updateUserDto.ToEntity(existingUser, passwordHash);
        await _context.SaveChangesAsync();

        return Ok(existingUser.ToDto());
    }

    // DELETE api/auth/users/{id}
    [HttpDelete("users/{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        var hashedInputPassword = HashPassword(password);
        return hashedInputPassword == passwordHash;
    }
}
