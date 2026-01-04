using Xunit;
using Moq;
using fin_backend.Services;
using fin_backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Text;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;
    private readonly Mock<IConfiguration> _mockConfig;

    public JwtServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"JWTSettings:securityKey", "super_secret_key_123456789101112"},
            {"JWTSettings:validIssuer", "TestIssuer"},
            {"JWTSettings:validAudience", "TestAudience"},
            {"JWTSettings:expiryInMinutes", "60"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _jwtService = new JwtService(configuration);
    }

    [Fact]
    public void GetSigningCredentials_ReturnsSigningCredentials()
    {
        var creds = _jwtService.GetSigningCredentials();
        Assert.NotNull(creds);
        Assert.IsType<SigningCredentials>(creds);
    }

    [Fact]
    public void GetClaims_ReturnsClaimsForUser()
    {
        var user = new User { Id = 1, Username = "testuser", Role = "Admin" };
        var claims = _jwtService.GetClaims(user);

        Assert.Contains(claims, c => c.Type == ClaimTypes.Name && c.Value == "testuser");
        Assert.Contains(claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
    }

    [Fact]
    public void GenerateTokenOptions_ReturnsJwtSecurityToken()
    {
        var creds = _jwtService.GetSigningCredentials();
        var user = new User { Id = 1, Username = "testuser", Role = "Admin" };
        var claims = _jwtService.GetClaims(user);

        var token = _jwtService.GenerateTokenOptions(creds, claims);

        Assert.NotNull(token);
        Assert.IsType<JwtSecurityToken>(token);
    }

    [Fact]
    public void GenerateJwtToken_ReturnsTokenString()
    {
        var user = new User { Id = 1, Username = "testuser", Role = "Admin" };
        var token = _jwtService.GenerateJwtToken(user);

        Assert.False(string.IsNullOrEmpty(token));
        Assert.Contains(".", token); // JWTs have at least two dots
    }
}