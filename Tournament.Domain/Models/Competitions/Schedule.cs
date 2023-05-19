using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competitions;

public class Schedule : BaseEntity<int>
{
    public Guid FirstPlayerId { get; set; }
    
    public Guid SecondPlayerId { get; set; }
    
    public bool HasPlayed { get; set; }
    
    public int TableNumber { get; set; }
    
    public int RoundNumber { get; set; }
    
    public int FirstPlayerScore { get; set; }
    
    public int SecondPlayerScore { get; set; }
    
    public Guid CompetitionId { get; set; }
    
    public Competition? Competition { get; set; }
}