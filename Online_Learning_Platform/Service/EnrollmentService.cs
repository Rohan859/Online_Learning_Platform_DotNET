using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Enums;
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


        public string UnEnroll(Guid userId, Guid courseId)
        {
            //1. find the user and course
            
            
            
            

            var user = _dbContext.Users.Find(userId);
            var course = _dbContext.Courses.Find(courseId);

            //2. validate them
            if (user == null || course==null)
            {
                return "User or course not exist";
            }

            //3. remove the course from the user's course list
            var courseList = user.Courses;

            foreach (var item in courseList)
            {
                if(item==course)
                {
                    courseList.Remove(item);
                    break;
                }
            }

            //4. put null in user in course entity
            course.User = null;

            //5.save changes

            _dbContext.Users.Update(user);
            _dbContext.Courses.Update(course);

            _dbContext.SaveChanges();

            return "Successfully Unenrolled";
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


        List<Enrollment>GetAllTheEnrollments(Guid courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return null;
            }

            return course.Enrollments;
        }

        public List<Enrollment>TrackProgress(Progress progress)
        {
            var list = _dbContext.Enrollments
                .Where(e => e.Progress == progress)
                .ToList();

            return list;
        }
    }
}
