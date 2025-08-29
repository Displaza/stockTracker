using fin_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace fin_backend.Services
{
    public interface IJwtService
    {
        SigningCredentials GetSigningCredentials();
        List<Claim> GetClaims(User user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        string GenerateJwtToken(User user);
    }
}
