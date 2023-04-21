using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto;

public class RegisterRoleModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}