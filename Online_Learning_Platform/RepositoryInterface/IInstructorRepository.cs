using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IInstructorRepository
    {
        public void SaveToInstructorDb(Instructor instructor);
        public void Save();
        public Instructor FindInstructorById(Guid instructorId);
        public Instructor FindInstructorByIdAndIncludeCourse(Guid instructorId);
        public void DeleteInstructor(Instructor instructor);
        public List<Instructor> FindListOfInstructorsByCourseId(Guid courseId);
        public void UpdateInstructor(Instructor instructor);


    }
}
