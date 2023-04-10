using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tournament.Models;
using Tournament.Options;
using Tournament.Services;

namespace Tournament.Implementation;

public class TokenService : ITokenService
{
    private readonly JwtOption _jwtSetting;
    private readonly IParticipantService _participantService;
    
    public TokenService(IOptionsSnapshot<JwtOption> optionsSnapshot, IParticipantService participantService)
    {
        _jwtSetting = optionsSnapshot.Value;
        _participantService = participantService;
    }
    
    private string GenerateToken(Participant participant, int expirationTime)
    {
        var claims = new[] {  
            new Claim(ClaimTypes.Name, participant.FirstName),
            new Claim(ClaimTypes.Email, participant.Email),
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key)); 
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(expirationTime), 
            signingCredentials: credentials);  
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string GenerateAccessToken(Participant participant)
    {
        return GenerateToken(participant, _jwtSetting.AccessTokenExpiryDurationMinutes);
    }
    
    public string GenerateRefreshToken(Participant participant)
    {
        return GenerateToken(participant, _jwtSetting.RefreshTokenExpiryDurationMinutes);
    }
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, 
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key)),
            ValidateLifetime = false 
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}