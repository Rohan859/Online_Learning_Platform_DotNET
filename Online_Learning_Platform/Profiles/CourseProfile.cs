using AutoMapper;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDetailsUpdateDTO, Course>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.CourseName, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.CourseDescription, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)))
                .ForMember(dest => dest.Price, opt => opt.Condition((src, dest, srcMember) => srcMember > 0))
                .ForMember(dest => dest.StartDate, opt => opt.Condition((src, dest, srcMember) =>
                {
                    return !(srcMember < DateTime.Today || srcMember == DateTime.MinValue);
                }))
                .ForMember(dest => dest.EndDate, opt => opt.Condition((src, dest, srcMember) =>
                {
                    return !(srcMember < DateTime.Today || srcMember == DateTime.MinValue);
                }));




        }
    }
}
