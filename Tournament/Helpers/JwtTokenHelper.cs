// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;
//
// namespace Tournament.Helpers;
//
// public static class JwtTokenHelper
// {
//     /// <summary>
//     /// Generate JWT Token after successful login.
//     /// </summary>
//     /// <param name="accountId"></param>
//     /// <returns></returns>
//     private static string GenerateJwtToken(string userName)
//     {
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
//             Expires = DateTime.UtcNow.AddHours(1),
//             Issuer = _configuration["Jwt:Issuer"],
//             Audience = _configuration["Jwt:Audience"],
//             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//         };
//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         return tokenHandler.WriteToken(token);
//     }
// }