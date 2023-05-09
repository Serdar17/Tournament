using System.ComponentModel.DataAnnotations;

namespace Tournament.Application.Dto;

public class RegisterRoleModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}