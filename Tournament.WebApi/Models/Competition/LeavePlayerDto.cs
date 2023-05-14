using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Competitions.Commands.LeaveFromCompetition;

namespace Tournament.Models.Competition;

public class LeavePlayerDto : IMapWith<LeavePlayerCompetitionCommand>
{
    public Guid ParticipantId { get; set; }
    
    public Guid CompetitionId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LeavePlayerDto, LeavePlayerCompetitionCommand>()
            .ForMember(p => p.ParticipantId,
                opt => opt.MapFrom(info => info.ParticipantId))
            .ForMember(infoVm => infoVm.CompetitionId,
                opt => opt.MapFrom(info => info.CompetitionId));
    }
}