using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto.Auth;

public class TokenApiModel
{
    [Required]
    public string? AccessToken { get; set; }
    
    [Required]
    public string? RefreshToken { get; set; }
}