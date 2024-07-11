using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AllTheDbContext _theDbContext;
        public InstructorRepository(AllTheDbContext allTheDbContext)
        {
            _theDbContext = allTheDbContext;
        }

        public void DeleteInstructor(Instructor instructor)
        {
            _theDbContext.Instructors.Remove(instructor);
        }

        public Instructor? FindInstructorById(Guid instructorId)
        {
            var instructor = _theDbContext.Instructors.Find(instructorId);
            return instructor;
        }

        public Instructor? FindInstructorByIdAndIncludeCourse(Guid instructorId)
        {
            var instructor = _theDbContext.Instructors
                .Include(e => e.Course)
                .FirstOrDefault(e => e.InstructorId == instructorId);

            return instructor;
        }

        public List<Instructor> FindListOfInstructorsByCourseId(Guid courseId)
        {
            List<Instructor> instructorList = _theDbContext.Instructors
                .Where(x => x.CourseId == courseId)
                .ToList();

            return instructorList;
        }

        public void Save()
        {
            _theDbContext.SaveChanges();
        }

        public void SaveToInstructorDb(Instructor instructor)
        {
            _theDbContext.Instructors.Add(instructor);
        }


    }
}
