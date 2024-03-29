﻿using AutoMapper;
using Tournament.Application.Dto.Account;
using Tournament.Application.Dto.Auth;
using Tournament.Domain.Models.Participants;

namespace Tournament.Application.Common.Mappings;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        CreateMap<RegisterModel, ApplicationUser>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.SportsCategory, opt => opt.MapFrom(src => src.SportsCategory));

        CreateMap<ApplicationUser, ParticipantInfoModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.SchoolNumber, opt => opt.MapFrom(src => src.SchoolNumber))
            .ForMember(dest => dest.CurrentRating, opt => opt.MapFrom(src => src.Rating))
            .ReverseMap();
    }
}