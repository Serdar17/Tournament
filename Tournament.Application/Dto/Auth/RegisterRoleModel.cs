using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto.Auth;

public class RegisterRoleModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}