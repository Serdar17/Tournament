using System.ComponentModel.DataAnnotations;
using Tournament.Domain.Enums;

namespace Tournament.Application.Dto.Account;

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
    public int Age { get; set; }

    [Required]
    public int SchoolNumber { get; set; }
    
    [Required]
    public string SportsCategory { get; set; }
    
    [Required] 
    public int CurrentRating { get; set; }

    public List<MatchResultModel> MatchResultModels { get; set; } = new();
}