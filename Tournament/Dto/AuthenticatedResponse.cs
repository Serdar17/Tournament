using System.ComponentModel.DataAnnotations;

namespace Tournament.Dto;

public class TokenApiModel
{
    [Required]
    public string? AccessToken { get; set; }
    
    [Required]
    public string? RefreshToken { get; set; }
}