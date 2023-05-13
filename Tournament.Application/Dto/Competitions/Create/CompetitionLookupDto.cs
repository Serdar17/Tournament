using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Dto.Competitions.Create;

public class CompetitionLookupDto : IMapWith<Competition>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDateTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Competition, CompetitionLookupDto>()
            .ForMember(infoVm => infoVm.Id,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.Title,
                opt => opt.MapFrom(info => info.Title))
            .ForMember(infoVm => infoVm.StartDateTime,
                opt => opt.MapFrom(info => info.StartDateTime));
    }
}