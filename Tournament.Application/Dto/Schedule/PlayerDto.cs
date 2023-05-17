using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Dto.Competitions;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Schedule;

public class PlayerDto : IMapWith<Player>
{
    public Guid PlayerId { get; set; }

    public string FirstName { get; set; }
    
    public string MiddleName { get; set; }
    
    public string LastName { get; set; }
    
    public int CurrentRating { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, PlayerDto>()
            .ForMember(infoVm => infoVm.FirstName,
                opt => opt.MapFrom(info => info.ApplicationUser!.FirstName))
            .ForMember(infoVm => infoVm.MiddleName,
                opt => opt.MapFrom(info => info.ApplicationUser!.MiddleName))
            .ForMember(infoVm => infoVm.LastName,
                opt => opt.MapFrom(info => info.ApplicationUser!.LastName))
            .ForMember(infoVm => infoVm.PlayerId,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.CurrentRating,
                opt => opt.MapFrom(info => info.CurrentRating));
    }
}