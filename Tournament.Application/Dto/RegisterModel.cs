using System.ComponentModel.DataAnnotations;
using Tournament.Domain.Enums;
using Tournament.Enums;

namespace Tournament.Dto;

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
    public DateTime BirthDate { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get;  set; }
    
    [Required] 
    public string UserName { get; set; }
    
    [Required]
    // [MinLength(8, ErrorMessage = "The password must be longer than 8 characters")]
    public string Password { get; set; }
    
    [Required]
    public int SchoolNumber { get; set; }

    [Required]
    public SportsCategory SportsCategory { get;  set; }

    [Required]
    public long Rating { get; set; }
}