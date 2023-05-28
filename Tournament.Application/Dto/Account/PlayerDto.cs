using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;

namespace Tournament.Application.Dto.Account;

public class PlayerModel : IMapWith<ApplicationUser>
{
    public string FirstName { get; set; }
    
    public string MiddleName { get; set; }
    
    public string LastName { get; set; }
    
    public int CurrentRating { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Player, PlayerModel>()
            .ForMember(u => u.FirstName,
                opt => opt.MapFrom(info => info.ApplicationUser!.FirstName))
            .ForMember(u => u.MiddleName,
                opt => opt.MapFrom(info => info.ApplicationUser!.MiddleName))
            .ForMember(u => u.LastName,
                opt => opt.MapFrom(info => info.ApplicationUser!.LastName))
            .ForMember(u => u.CurrentRating,
                opt => opt.MapFrom(info => info.CurrentRating));
    }
}