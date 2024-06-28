using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class EnrollmentService
    {
        private readonly AllTheDbContext _dbContext;

        public EnrollmentService(AllTheDbContext context)
        {
            _dbContext = context;
        }


        public string EnrollInACourse(Guid userId,Guid courseId)
        {
            //1. validate the user and course
            var user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }


            //2. generate new enrollmrnt id
            var enrollment = new Enrollment();
            enrollment.EnrollmentId = Guid.NewGuid();

            //3. make the enrollment
            enrollment.Progress = Enums.Progress.Ongoing;
            enrollment.EnrollmentDate = DateTime.UtcNow;

            //4. in user's course list add the new course
            enrollment.Course.User= user;
            user.Courses.Add(course);

            _dbContext.Enrollments.Add(enrollment);
            _dbContext.Users.Update(user);
            _dbContext.Courses.Update(course);
            _dbContext.SaveChanges();

            

            return $"Your enrollment is successfull, enrollment id is {enrollment.EnrollmentId}";
        }


        public string DeleteEnrollment(Guid enrollmentId)
        {
            var enrollment = _dbContext.Enrollments.Find(enrollmentId);

            if(enrollment == null)
            {
                return "Not Found";
            }

            _dbContext.Enrollments.Remove(enrollment);
            _dbContext.SaveChanges();

            return "Successfully deleted the enrollment";
        }
    }
}
