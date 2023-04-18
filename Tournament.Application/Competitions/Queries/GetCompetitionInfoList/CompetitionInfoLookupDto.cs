using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoList;

public class CompetitionInfoLookupDto : IMapWith<CompetitionInfo>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime StartDateTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CompetitionInfo, CompetitionInfoLookupDto>()
            .ForMember(infoVm => infoVm.Id,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.Title,
                opt => opt.MapFrom(info => info.Title))
            .ForMember(infoVm => infoVm.StartDateTime,
                opt => opt.MapFrom(info => info.StartDateTime));
    }
}