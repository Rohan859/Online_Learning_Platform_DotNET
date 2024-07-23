using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AllTheDbContext _theDbContext;
        private readonly IMemoryCache _cache;
        public InstructorRepository(AllTheDbContext allTheDbContext,
            IMemoryCache cache)
        {
            _theDbContext = allTheDbContext;
            _cache = cache;
        }

        public void DeleteInstructor(Instructor instructor)
        {
            _theDbContext.Instructors.Remove(instructor);
        }

        public Instructor? FindInstructorById(Guid instructorId)
        {
            return _cache.GetOrCreate($"Instructor_{instructorId}", entry =>
            {
                //cache is valid for only 5 minutes
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

                var instructor = _theDbContext.Instructors.Find(instructorId);

                return instructor;
            });
            //var instructor = _theDbContext.Instructors.Find(instructorId);
            //return instructor;
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


        public void UpdateInstructor(Instructor instructor)
        {
            _theDbContext.Instructors.Update(instructor);
        }

    }
}
