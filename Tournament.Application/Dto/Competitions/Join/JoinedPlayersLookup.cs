using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Enums;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Competitions.Join;

public class JoinedPlayersLookup : IMapWith<Player>
{
    public Guid PlayerId { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public int CurrentRating { get; set; }

    public int Scored { get; set; }
    
    public int Missed { get; set; }

    public int WinGameCount { get; set; }
    
    public int LoseGameCount { get; set; }
    
    public Gender Gender { get; set; } 
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, JoinedPlayersLookup>()
            .ForMember(infoVm => infoVm.FirstName,
                opt => opt.MapFrom(info => info.ApplicationUser!.FirstName))
            .ForMember(infoVm => infoVm.MiddleName,
                opt => opt.MapFrom(info => info.ApplicationUser!.MiddleName))
            .ForMember(infoVm => infoVm.LastName,
                opt => opt.MapFrom(info => info.ApplicationUser!.LastName))
            .ForMember(infoVm => infoVm.Gender,
                opt => opt.MapFrom(info => info.ApplicationUser!.Gender))
            .ForMember(infoVm => infoVm.PlayerId,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.CurrentRating,
                opt => opt.MapFrom(info => info.CurrentRating))
            .ForMember(infoVm => infoVm.Scored,
                opt => opt.MapFrom(info => info.Scored))
            .ForMember(infoVm => infoVm.Missed,
                opt => opt.MapFrom(info => info.Missed))
            .ForMember(infoVm => infoVm.WinGameCount,
                opt => opt.MapFrom(info => info.WinGameCount))
            .ForMember(infoVm => infoVm.LoseGameCount,
                opt => opt.MapFrom(info => info.LoseGameCount))
            ;
    }
}