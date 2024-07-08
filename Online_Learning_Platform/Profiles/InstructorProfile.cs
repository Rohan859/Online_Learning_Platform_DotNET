using AutoMapper;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Profiles
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<InstructorUpdateRequestDTO, Instructor>()
                .ForMember(dest => dest.InstructorId, opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.InstructorName, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Password, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Email, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Description, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)));




        }
    }
}
