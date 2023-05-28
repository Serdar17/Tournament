using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competitions;

public sealed class Competition : BaseEntity<Guid>
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public int TableCount { get; set; }
    
    public int RoundsCount { get; set; }
    
    public DateTime CreationDateTime { get; set; }

    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }
    
    public List<Player> Players { get; set; } = new();

    public List<MatchResult> MatchResults { get; set; } = new();

    public List<Schedule> Schedules { get; set; } = new();

    public void UpdateFields(string title, string description, DateTime startDateTime, 
        string placeDescription, int tableCount, int roundsCount)
    {
        Title = title;
        Description = description;
        StartDateTime = startDateTime;
        PlaceDescription = placeDescription;
        TableCount = tableCount;
        RoundsCount = roundsCount;
    }
}