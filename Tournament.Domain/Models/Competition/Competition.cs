using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competition;

public sealed class Competition : BaseEntity<Guid>
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public DateTime CreationDateTime { get; set; }

    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }

    public string? Result { get; set; }

    public List<Player> Players { get; set; } = new();
}