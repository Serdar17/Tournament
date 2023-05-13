using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Competitions.Queries.GetCompetitonPlayers;

public class CompetitionPlayersLookup : IMapWith<Player>
{
    public Guid PlayerId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public int CurrentRating { get; set; }

    public int Scored { get; set; }

    public int Missed { get; set; }
}