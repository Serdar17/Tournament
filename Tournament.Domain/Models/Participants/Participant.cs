using Microsoft.AspNetCore.Identity;
using Tournament.Domain.Enums;

namespace Tournament.Domain.Models.Participants;

public sealed class Participant : IdentityUser
{
    public string FirstName { get; set; }
    
    public string MiddleName { get; set ; }
    
    public string LastName { get;  set; }
    
    public Gender Gender { get; set; }
    
    public int Age { get; set; }
    
    public int SchoolNumber { get; set; }

    public string SportsCategory { get;  set; }

    public long Rating { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}