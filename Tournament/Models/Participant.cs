using System.Diagnostics.CodeAnalysis;
using Tournament.Enums;
using Tournament.Primitives;

namespace Tournament.Models;

public sealed class Participant: BaseEntity
{
    // public Participant(int id, string firstName, string middleName, string lastName, 
    //     string email, string password, Gender gender, int schoolNumber, DateTime birthDate,
    //     SportsCategory sportsCategory, long rating) : base(id)
    // {
    //     FirstName = firstName;
    //     MiddleName = middleName;
    //     LastName = lastName;
    //     Email = email;
    //     Password = password;
    //     Gender = gender;
    //     BirthDate = birthDate;
    //     SchoolNumber = schoolNumber;
    //     SportsCategory = sportsCategory;
    //     Rating = rating;
    // }
    

    public string FirstName { get; set; }
    
    public string MiddleName { get; set ; }
    
    public string LastName { get;  set; }
    
    public string Phone { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string Email { get;  set; }
    
    public string Password { get; set; }
    
    public int SchoolNumber { get; set; }

    public SportsCategory SportsCategory { get;  set; }

    public long Rating { get; set; }

}