using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto;

public class LoginModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}