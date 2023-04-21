using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Tournament.Options;

namespace Tournament.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtOption _jwtSetting;

    public JwtMiddleware(RequestDelegate next, IOptions<JwtOption> optionsSnapshot)
    {
        _next = next;
        _jwtSetting = optionsSnapshot.Value;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachAccountToContext(context, token);

        await _next(context);
    }
    
    private void AttachAccountToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;

            // attach account to context on successful jwt validation
            // context.Items["User"] = _userService.GetUserDetails();
            context.Items["User"] = accountId;
        }
        catch
        {
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
            // TODO: Сделать редирект на странцу регистрации
        }
    }
}