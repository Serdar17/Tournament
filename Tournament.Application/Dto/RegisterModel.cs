using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;
using Tournament.Domain.Enums;

namespace Tournament.Application.Dto;

public class RegisterModel
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string MiddleName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required] 
    // [PhoneValidation]
    public string PhoneNumber { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Required]
    // [Range(0, 100, ErrorMessage = "The age field must in range from 0 to 100")]
    public int Age { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get;  set; }

    [Required]
    [LogMasked(PreserveLength = true)]
    // [MinLength(8, ErrorMessage = "The password must be longer than 8 characters")]
    public string Password { get; set; }
    
    [Required]
    public string SportsCategory { get;  set; }
}