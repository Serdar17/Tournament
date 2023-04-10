using System.ComponentModel.DataAnnotations;
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
    public string PhoneNumber { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public string Email { get;  set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public int SchoolNumber { get; set; }

    [Required]
    public SportsCategory SportsCategory { get;  set; }

    [Required]
    public long Rating { get; set; }
}