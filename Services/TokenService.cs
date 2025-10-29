using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CursoInfoeste.Services;

public class TokenService(IConfiguration config, Persistencia persistencia)
{
    public string GenerateToken()
    {
        var claims = new List<Claim>()
        {
            new Claim("TenantId", persistencia.TenantId.ToString()),
            new Claim("Nome", "Infoeste"),
        };

        var secret = config.GetValue<string>("Security:Jwt:Secret");
        var audience = config.GetValue<string>("Security:Jwt:Audience");
        var issuer = config.GetValue<string>("Security:Jwt:Issuer");
        var expiresIn = config.GetValue<int>("Security:Jwt:ExpiresIn");

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expiresIn),
            audience:audience, 
            issuer: issuer, 
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}