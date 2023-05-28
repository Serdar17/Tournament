using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Dto.Account;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Competitions.ConfirmedMatchResult;

public class ConfirmedMatchResultLookup : IMapWith<Schedule>
{
    public int ScheduleId { get; set; }
    
    public Guid FirstPlayerId { get; set; }

    public PlayerModel? FirstPlayerModel { get; set; }
    
    public Score? FirstPlayerScore { get; set; }
    
    public Guid SecondPlayerId { get; set; }

    public PlayerModel? SecondPlayerModel { get; set; }
    
    public Score? SecondPlayerScore { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Competitions.Schedule, ConfirmedMatchResultLookup>()
            .ForMember(infoVm => infoVm.ScheduleId,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.FirstPlayerId,
                opt => opt.MapFrom(info => info.FirstPlayerId))
            .ForMember(infoVm => infoVm.SecondPlayerId,
                opt => opt.MapFrom(info => info.SecondPlayerId))
            .ForMember(infoVm => infoVm.FirstPlayerScore,
                opt => opt.MapFrom(info => info.FirstPlayerScored))
            .ForMember(infoVm => infoVm.SecondPlayerScore,
                opt => opt.MapFrom(info => info.SecondPlayerScored));
    }
}