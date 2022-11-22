using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FarmerApp.Api.Services;

public class UserService
{
    private readonly FarmerDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    public User CurrentUser;

    public UserService(FarmerDbContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string GetMyName()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        return result;
    }

    public async Task<User> AddUserAsync(UserVM user)
    {
        CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = await _db.Users.AddAsync(new User
        {
            Username = user.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = Role.Admin,
        });

        return newUser.Entity;
    }

    public async Task<User?> GetByPasswordAsync(string password)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = await _db.Users.FirstOrDefaultAsync(u =>
            u.PasswordHash == passwordHash && u.PasswordSalt == passwordSalt);

        CurrentUser = user;
        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
    
    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public async Task<User?> GetByRefreshToken(string? refreshToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        return user;
    }

    public async Task AddNewRefreshToken(User user,RefreshToken newRefreshToken)
    {
        var refreshUser = user;

        refreshUser.RefreshToken = newRefreshToken.Token;
        refreshUser.TokenCreated = newRefreshToken.Created;
        refreshUser.TokenExpires = newRefreshToken.Expires;
        
        _db.Users.Update(refreshUser);
    }
}