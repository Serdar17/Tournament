using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class RefereePlayerLookup : IMapWith<Player>
{
    public Guid PlayerId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public int CurrentRating { get; set; }

    public bool IsParticipation { get; set; }
    
    public bool IsBlocked { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, RefereePlayerLookup>()
            .ForMember(infoVm => infoVm.PlayerId,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.CurrentRating,
                opt => opt.MapFrom(info => info.CurrentRating))
            .ForMember(infoVm => infoVm.IsParticipation,
                opt => opt.MapFrom(info => info.IsParticipation))
            .ForMember(infoVm => infoVm.IsBlocked,
                opt => opt.MapFrom(info => info.IsBlocked));
    }
}