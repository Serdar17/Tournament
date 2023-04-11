using System.Security.Claims;
using Tournament.Models;

namespace Tournament.Services;

public interface ITokenService
{
    // public string GenerateAccessToken(Participant participant);

    public string GenerateToken(List<Claim> claims);

    public string GenerateRefreshToken();

    // public string GenerateRefreshToken(Participant participant);

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}