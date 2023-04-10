using System.ComponentModel.DataAnnotations;

namespace Tournament.Dto;

public class LoginModelDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}