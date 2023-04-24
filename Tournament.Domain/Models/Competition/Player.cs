using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competition;

public sealed class Player : BaseEntity<Guid>
{
    public long CurrentRating { get; set; }
    
    public bool IsParticipation { get; set; }

    public int Scored { get; set; }
    
    public int Missed { get; set; }

    public string ParticipantId { get; set; }
    
    public Guid CompetitonId { get; set; }
    
    public Competition? Competition { get; set; }

    public List<Player> Players { get; set; } = new();
}