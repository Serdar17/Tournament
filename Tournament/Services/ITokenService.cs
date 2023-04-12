using System.Security.Claims;

namespace Tournament.Services;

public interface ITokenService
{
    public string GenerateToken(List<Claim> claims);

    public string GenerateRefreshToken();
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}