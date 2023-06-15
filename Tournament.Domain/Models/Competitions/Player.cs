using Tournament.Domain.Models.Participants;
using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competitions;

public sealed class Player : BaseEntity<Guid>  
{
    public int CurrentRating { get; set; }
    
    public bool IsParticipation { get; set; }

    public bool IsBlocked { get; set; }

    public int Scored { get; set; }
    
    public int Missed { get; set; }

    public int WinGameCount { get; set; }
    
    public int LoseGameCount { get; set; }

    public int Total => Scored + Missed;

    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    
    public Guid CompetitionId { get; set; }
    
    public Competition? Competition { get; set; }

    public List<Guid> PlayedGames { get; set; } = new();

    public void SetScore(int score, int missed)
    {
        if (score > missed)
            WinGameCount += 1;
        else
            LoseGameCount += 1;
        
        Scored += score;
        Missed += missed;
    }
}