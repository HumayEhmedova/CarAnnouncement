using AutoMapper;
using CarAnnouncementProject.DTOs.Announcement;
using CarAnnouncementProject.DTOs.AnnouncementDtos;
using CarAnnouncementProject.Models;
using System.Reflection.Metadata;

namespace CarAnnouncementProject.Mappers
{
    public class AnnouncementMapper : Profile
    {
        public AnnouncementMapper()
        {
            CreateMap<Announcement, AnnouncementGetDto>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl)))
            .ForMember(dest => dest.MarkaId, opt => opt.MapFrom(src => src.Model.MarkaId));

            CreateMap<Announcement, AnnouncementGetByIdDto>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl)))
            .ForMember(dest => dest.MarkaId, opt => opt.MapFrom(src => src.Model.MarkaId));
        }
    }
}
