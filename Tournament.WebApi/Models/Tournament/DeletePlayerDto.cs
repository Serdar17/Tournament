using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Features.Players.Commands.DeletePlayer;

namespace Tournament.Models.Tournament;

public class DeletePlayerDto : IMapWith<DeletePlayerCommand>
{
    public Guid ParticipantId { get; set; }

    public Guid CompetitionId { get; set; }

    public Guid PlayerId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeletePlayerDto, DeletePlayerCommand>()
            .ForMember(p => p.ParticipantId,
                opt => opt.MapFrom(info => info.ParticipantId))
            .ForMember(infoVm => infoVm.CompetitionId,
                opt => opt.MapFrom(info => info.CompetitionId))
            .ForMember(infoVm => infoVm.PlayerId,
            opt => opt.MapFrom(info => info.PlayerId));
    }
}