using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competition;

public class Competition : BaseEntity<Guid>
{
    public string ParticipantId { get; set; }

    public Guid CompetitonId { get; set; }

    public long CurrentRating { get; set; }

    public bool IsParticipation { get; set; }
    
}