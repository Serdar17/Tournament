using System.Security.Claims;

namespace Tournament.Application.Interfaces;

public interface ITokenService
{
    public string GenerateToken(List<Claim> claims);

    public string GenerateRefreshToken();
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}