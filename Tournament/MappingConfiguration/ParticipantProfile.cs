using AutoMapper;
using Tournament.Dto;
using Tournament.Models;

namespace Tournament.MappingConfiguration;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<RegisterModel, Participant>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.SchoolNumber, opt => opt.MapFrom(src => src.SchoolNumber))
            .ForMember(dest => dest.SportsCategory, opt => opt.MapFrom(src => src.SportsCategory))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
    }
}