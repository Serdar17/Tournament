using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Competitions.Commands.CreateCompetition;

namespace Tournament.Models.Competition;

public class CreateCompetitionDto : IMapWith<CreateCompetitionCommand>
{
    [Required]
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int TableCount { get; set; }
    
    public int RoundsCount { get; set; }
    
    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCompetitionDto, CreateCompetitionCommand>()
            .ForMember(infoVm => infoVm.Title,
                opt => opt.MapFrom(info => info.Title))
            .ForMember(infoVm => infoVm.Description,
                opt => opt.MapFrom(info => info.Description))
            .ForMember(infoVm => infoVm.StartDateTime,
                opt => opt.MapFrom(info => info.StartDateTime))
            .ForMember(infoVm => infoVm.PlaceDescription,
                opt => opt.MapFrom(info => info.PlaceDescription))
            .ForMember(infoVm => infoVm.TableCount,
                opt => opt.MapFrom(info => info.TableCount))
            .ForMember(infoVm => infoVm.RoundsCount,
                opt => opt.MapFrom(info => info.RoundsCount));
    }
}