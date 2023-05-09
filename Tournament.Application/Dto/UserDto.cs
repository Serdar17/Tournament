using AutoMapper;
using Tournament.Application.Common.Mappings;
using Tournament.Domain.Enums;
using Tournament.Domain.Models.Participants;

namespace Tournament.Application.Dto;

public class UserDto : IMapWith<Participant>
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string MiddleName { get; set ; }
    
    public string LastName { get;  set; }
    
    public Gender Gender { get; set; }
    
    public int Age { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public int SchoolNumber { get; set; }
    
    public string SportsCategory { get;  set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Participant, UserDto>()
            .ForMember(u => u.Id,
                opt => opt.MapFrom(info => info.Id))
            .ForMember(u => u.FirstName,
                opt => opt.MapFrom(info => info.FirstName))
            .ForMember(u => u.MiddleName,
                opt => opt.MapFrom(info => info.MiddleName))
            .ForMember(u => u.LastName,
                opt => opt.MapFrom(info => info.LastName))
            .ForMember(u => u.Gender,
                opt => opt.MapFrom(info => info.Gender))
            .ForMember(u => u.SchoolNumber,
                opt => opt.MapFrom(info => info.SchoolNumber))
            .ForMember(u => u.SportsCategory,
                opt => opt.MapFrom(info => info.SportsCategory))
            .ForMember(u => u.PhoneNumber,
                opt => opt.MapFrom(info => info.PhoneNumber))
            .ReverseMap();
            // .ForMember(x => x.Id, x => x.Ignore())
    }
}