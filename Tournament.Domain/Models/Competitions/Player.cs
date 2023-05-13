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

    public string? ParticipantId { get; set; }

    public ApplicationUser? Participant { get; set; }
    
    public Guid CompetitionId { get; set; }
    
    public Competition? Competition { get; set; }

    public List<Guid> PlayedGames { get; set; } = new();
}