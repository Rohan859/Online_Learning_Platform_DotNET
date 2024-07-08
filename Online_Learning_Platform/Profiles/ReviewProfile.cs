using AutoMapper;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewRequestDTO, Review>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Description, opt => opt.Condition((src, dest, srcMember) => !string.IsNullOrEmpty(srcMember)));

        }
    }
}
