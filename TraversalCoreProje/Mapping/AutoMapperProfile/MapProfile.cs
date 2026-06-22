using AutoMapper;
using DTOLayer.DTOs.About2DTOs;
using DTOLayer.DTOs.AboutDTOs;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.CityDTOs;
using DTOLayer.DTOs.ContactDTOs;
using DTOLayer.DTOs.DestinationDTOs;
using DTOLayer.DTOs.FeatureDTOs;
using DTOLayer.DTOs.GuideDTOs;
using EntityLayer.Concrete;

namespace TraversalCoreProje.Mapping.AutoMapperProfile
{
    public class MapProfile:Profile 
    {
        public MapProfile()
        {
            CreateMap<AnnouncementAddDto, Announcement>();
            CreateMap<Announcement, AnnouncementAddDto>();

            CreateMap<AppUserRegisterDTOs, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTOs>();

            CreateMap<AppUserLoginDTOs, AppUser>();
            CreateMap<AppUser, AppUserLoginDTOs>();

            CreateMap<AnnouncementListDto, Announcement>();
            CreateMap<Announcement, AnnouncementListDto>();

            CreateMap<AnnouncementUpdateDto, Announcement>();
            CreateMap<Announcement, AnnouncementUpdateDto>();

            CreateMap<SendMessageDto, ContactUs>().ReverseMap();

            CreateMap<GuideAddEditDTO, Guide>();
            CreateMap<Guide, GuideAddEditDTO>();

            CreateMap<DestinationAddDTO, Destination>();
            CreateMap<Destination, DestinationAddDTO>();

            CreateMap<DestinationUpdateDTO, Destination>()
                .ForMember(dest => dest.DestinationImage, opt => opt.Ignore())
                .ForMember(dest => dest.CoverImage, opt => opt.Ignore())
                .ForMember(dest => dest.Image2, opt => opt.Ignore());
            CreateMap<Destination, DestinationUpdateDTO>();

            CreateMap<About2AddEditDTO, About2>();
            CreateMap<About2, About2AddEditDTO>();


            CreateMap<AboutAddEditDTO, About>();
            CreateMap<About, AboutAddEditDTO>();

            CreateMap<FeatureAddEditDTO,Feature>();
            CreateMap<Feature, FeatureAddEditDTO>();


        }
    }
}
