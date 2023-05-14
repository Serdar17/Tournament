using Microsoft.AspNetCore.Identity;
using Tournament.Domain.Enums;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Models.Participants;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    
    public string MiddleName { get; set ; }
    
    public string LastName { get;  set; }
    
    public Gender Gender { get; set; }
    
    public int Age { get; set; }
    
    public int SchoolNumber { get; set; }

    public string SportsCategory { get;  set; }

    public int Rating { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }

    public Player? Player { get; set; }

    public void SetRating()
    {
        switch (SportsCategory)
        {
            case SportsCategories.Beginner:
                Rating = Gender == Gender.Male
                    ? AverageRating.MaleBeginnerRating
                    : AverageRating.FemaleBeginnerRating;
                break;
            
            case SportsCategories.Amateur:
                Rating = Gender == Gender.Male
                    ? AverageRating.MaleAmateurRating
                    : AverageRating.FemaleAmateurRating;
                break;
            
            case SportsCategories.Category:
                Rating = Gender == Gender.Male
                    ? AverageRating.MaleCategoryRating
                    : AverageRating.FemaleCategoryRating;
                break;
            
            case SportsCategories.Ms:
                Rating = Gender == Gender.Male
                    ? AverageRating.MaleMsRating
                    : AverageRating.FemaleMsRating;
                break;
            
            default:
                Rating = Gender == Gender.Male
                    ? AverageRating.MaleBeginnerRating
                    : AverageRating.FemaleBeginnerRating;
                break;
        }
    }
}