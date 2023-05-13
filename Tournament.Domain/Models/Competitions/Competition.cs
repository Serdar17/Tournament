using Tournament.Domain.Models.Participants;
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
    
    public ApplicationUser ApplicationUser { get; set; }
    public List<Player> Players { get; set; } = new();

    public List<GameResult> GameResults { get; set; } = new();
}