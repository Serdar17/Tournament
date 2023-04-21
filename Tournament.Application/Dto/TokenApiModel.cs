using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto;

public class TokenApiModel
{
    [Required]
    public string? AccessToken { get; set; }
    
    [Required]
    public string? RefreshToken { get; set; }
}