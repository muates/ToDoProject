using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoProject.Core.Config;

namespace ToDoProject.Application.Helper;

public static class TokenHelper
{
    public static string GenerateToken(int userId, string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentConfig.JwtKey ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            EnvironmentConfig.JwtIssuer,
            EnvironmentConfig.JwtAudience,
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}