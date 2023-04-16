using System.ComponentModel.DataAnnotations;
using Tournament.Domain.Enums;

namespace Tournament.Application.Dto;

public class ParticipantInfoModel
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string MiddleName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required] 
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get;  set; }
    
    [Required]
    public int SchoolNumber { get; set; }
}