using Microsoft.EntityFrameworkCore;
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
                return "User not found";
            }

            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return "Course not found";
            }


            //2. generate new enrollmrnt id
            var enrollment = new Enrollment();
            enrollment.EnrollmentId = Guid.NewGuid();

            //3. make the enrollment
            enrollment.Progress = Enums.Progress.Ongoing;
            enrollment.EnrollmentDate = DateTime.UtcNow;

            //4. in user's course list add the new course
            enrollment.Course=course;
            enrollment.CourseId=courseId;

            //user.Courses.Add(course);

            if(user.StudentCourses==null)
            {
                user.StudentCourses = new List<StudentCourse>();
            }

            if(course.StudentCourses==null)
            {
                course.StudentCourses= new List<StudentCourse>();
            }

            //make the StudentCourse
            var studentCourse = new StudentCourse
            {
                User = user,
                UserId=user.UserId,
                Course=course,
                CourseId=course.CourseId
            };

            //add it into course and users StudentCourse list
            user.StudentCourses.Add(studentCourse);
            course.StudentCourses.Add(studentCourse);

            //5.add the enrollments list in course
            course.Enrollments.Add(enrollment);

            _dbContext.StudentCourses.Add(studentCourse);
            _dbContext.Enrollments.Add(enrollment);
            _dbContext.Courses.Update(course);
            _dbContext.Users.Update(user);

            _dbContext.SaveChanges();

            //_dbContext.Users.Update(user);
            //_dbContext.SaveChanges();

            //_dbContext.Update(course);
            //_dbContext.SaveChanges();

            

            return $"Your enrollment is successfull, enrollment id is {enrollment.EnrollmentId}";
        }


        public string UnEnroll(Guid enrollmentId)
        {

            // 1. Find the enrollment from the db
            //var enrollment = _dbContext.Enrollments
            //                            .Include(e => e.Course)
            //                                .ThenInclude(c => c.User)
            //                            .FirstOrDefault(e => e.EnrollmentId == enrollmentId);


            var enrollment = _dbContext.Enrollments
                                .Include(e => e.Course)
                                .ThenInclude(e => e.StudentCourses)
                                .FirstOrDefault(e => e.EnrollmentId == enrollmentId);
            // 2. Validate the enrollment
            if (enrollment == null)
            {
                return "Enrollment not found";
            }

            // 3. Access the course and user from the enrollment
            var course = enrollment.Course;

            if (course == null)
            {
                return "Course associated with enrollment is null";
            }

            //var user = course.User;

            var studentCourse = course.StudentCourses
                .FirstOrDefault(e => e.CourseId==course.CourseId);
            
            var user = _dbContext.Users.Find(studentCourse.UserId);

            if (user == null)
            {
                return "User associated with course is null";
            }

            try
            {
                // 4. Remove the course from the user's course list
                //user.Courses.Remove(course);

                user.StudentCourses.Remove(studentCourse);
                course.StudentCourses.Remove(studentCourse);

                // 5. Update references to null
                enrollment.Course = null;

                // 6. Delete the enrollment
                _dbContext.Enrollments.Remove(enrollment);
                _dbContext.StudentCourses.Remove(studentCourse);

                // 7. Save changes to the database
                _dbContext.SaveChanges();

                return "Successfully unenrolled the course";
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message
                return $"Error occurred: {ex.Message}";
            }
          
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
