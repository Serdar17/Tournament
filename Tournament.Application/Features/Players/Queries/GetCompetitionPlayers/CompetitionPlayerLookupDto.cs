using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Features.Players.Queries.GetCompetitionPlayers;

public class CompetitionPlayerLookupDto : IMapWith<Player>
{
    public Guid PlayerId { get; set; }
    
    public long CurrentRating { get; set; }
    
    public bool IsParticipation { get; set; }

    public int Scored { get; set; }
    
    public int Missed { get; set; }
    
    public List<Player> Players { get; set; } = new();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, CompetitionPlayerLookupDto>()
            .ForMember(infoVm => infoVm.PlayerId,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.CurrentRating,
                opt => opt.MapFrom(info => info.CurrentRating))
            .ForMember(infoVm => infoVm.Scored,
                opt => opt.MapFrom(info => info.Scored))
            .ForMember(infoVm => infoVm.Missed,
                opt => opt.MapFrom(info => info.Missed))
            .ForMember(infoVm => infoVm.Players,
                opt => opt.MapFrom(info => info.Players));
    }
    
}