﻿using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Competitions.Commands.CreateCompetitionInfo;

namespace Tournament.Models.Competition;

public class CreateCompetitionDto : IMapWith<CreateCompetitionInfoCommand>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCompetitionDto, CreateCompetitionInfoCommand>()
            .ForMember(infoVm => infoVm.Title,
                opt => opt.MapFrom(info => info.Title))
            .ForMember(infoVm => infoVm.Description,
                opt => opt.MapFrom(info => info.Description))
            .ForMember(infoVm => infoVm.StartDateTime,
                opt => opt.MapFrom(info => info.StartDateTime))
            .ForMember(infoVm => infoVm.PlaceDescription,
                opt => opt.MapFrom(info => info.PlaceDescription));
    }
}