using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;

namespace Tournament.Application.Dto;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [LogMasked(PreserveLength = true)]
    public string Password { get; set; }
}