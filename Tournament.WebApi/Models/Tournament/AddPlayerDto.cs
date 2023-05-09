using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Competitions.Commands.JoinPlayerCompetition;
using Tournament.Application.Features.Players.Commands.CreatePlayer;

namespace Tournament.Models.Tournament;

public class AddPlayerDto : IMapWith<CreatePlayerCommand>
{
    public Guid ParticipantId { get; set; }
    
    public Guid CompetitionId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddPlayerDto, JoinPlayerCompetitionCommand>()
            .ForMember(p => p.ParticipantId,
                opt => opt.MapFrom(info => info.ParticipantId))
            .ForMember(infoVm => infoVm.CompetitionId,
                opt => opt.MapFrom(info => info.CompetitionId));
    }
}