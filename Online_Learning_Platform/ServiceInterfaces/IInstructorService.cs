using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Interfaces
{
    public interface IInstructorService
    {
        public string Register(Instructor instructor);
        public string updateInstructor(InstructorUpdateRequestDTO instructorUpdateRequestDTO);
        public string RemoveInstructor(Guid id);
        public string AssignInstructor(Guid instructorId, Guid courseId);
        public int GetCountOfInstructorByCourseId(Guid courseId);
        public List<Instructor> GetListOfInstructorsByCourseId(Guid courseId);
       
    }
}
