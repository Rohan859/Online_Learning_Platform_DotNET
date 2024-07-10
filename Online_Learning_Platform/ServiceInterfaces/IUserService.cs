using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Interfaces
{
    public interface IUserService
    {
        public string Register(UserRegistrationRequestDTO userRegistrationRequestDTO);
        public string DeleteUserById(Guid id);
        public string UpdateUserProfile(UserProfileUpdateRequestDTO userProfileUpdateRequestDTO);
        public List<Course> GetCourseListForUserById(Guid userId);
        public int CountEnrollCoursesByUserId(Guid userId);
        public int GetNoOfReviewsByUserId(Guid userId);
    }
}
