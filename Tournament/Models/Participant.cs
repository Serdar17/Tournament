using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Tournament.Enums;
using Tournament.Primitives;

namespace Tournament.Models;

public sealed class Participant : IdentityUser
{
    public string FirstName { get; set; }
    
    public string MiddleName { get; set ; }
    
    public string LastName { get;  set; }
    
    public Gender Gender { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public int SchoolNumber { get; set; }

    public SportsCategory SportsCategory { get;  set; }

    public long Rating { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }

}