using Ardalis.Result;
using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Competitions.Queries.GetCompetitionDetails;

public class CompetitionVm : IMapWith<Competition>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDateTime { get; set; }
    
    public DateTime CreateDateTime { get; set; }

    public string PlaceDescription { get; set; }
    
    public int TableCount { get; set; }
    
    public int RoundsCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Competition, CompetitionVm>()
            .ForMember(infoVm => infoVm.Id,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(infoVm => infoVm.Title,
                opt => opt.MapFrom(info => info.Title))
            .ForMember(infoVm => infoVm.Description,
                opt => opt.MapFrom(info => info.Description))
            .ForMember(infoVm => infoVm.StartDateTime,
                opt => opt.MapFrom(info => info.StartDateTime))
            .ForMember(infoVm => infoVm.CreateDateTime,
                opt => opt.MapFrom(info => info.CreationDateTime))
            .ForMember(infoVm => infoVm.PlaceDescription,
                opt => opt.MapFrom(info => info.PlaceDescription))
            .ForMember(infoVm => infoVm.TableCount,
                opt => opt.MapFrom(info => info.TableCount))
            .ForMember(infoVm => infoVm.RoundsCount,
                opt => opt.MapFrom(info => info.RoundsCount));
    }
}