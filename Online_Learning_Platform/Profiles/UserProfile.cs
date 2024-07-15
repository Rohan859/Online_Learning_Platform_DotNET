using AutoMapper;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationRequestDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo));


            CreateMap<UserProfileUpdateRequestDTO, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Password, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Email, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.MobileNo, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)));






        }
    }
}
