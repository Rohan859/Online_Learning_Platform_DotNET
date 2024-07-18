using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using System.Text.Json;

namespace Online_Learning_Platform.Service
{
    public class EnrollmentService : IEnrollmentService
    {
       
       private readonly IEnrollmentRepository _enrollmentRepository;
       private readonly IUserRepository _userRepository;    
       private readonly ICourseRepository _courseRepository;

       private readonly IHttpClientFactory _httpClientFactory;
       

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IUserRepository userRepository,
            ICourseRepository courseRepository,
            IHttpClientFactory httpClientFactory)
        {
           _enrollmentRepository = enrollmentRepository;
           _userRepository = userRepository;   
           _courseRepository = courseRepository;
           _httpClientFactory = httpClientFactory;
        }


        private bool IsPresentTheCountry(string countryName)
        {

            try
            {
                // Create HttpClient instance
                using HttpClient client = _httpClientFactory.CreateClient();

                // Send synchronous GET request
                HttpResponseMessage response = client
                    .GetAsync("https://api.first.org/data/v1/countries").Result;

              

                // Read and deserialize JSON response synchronously
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var jsonDocument = JsonDocument.ParseAsync(responseStream).Result;

                // List to store country names
                List<string> countryNames = new List<string>();

                // Navigate through the JSON structure
                if (jsonDocument.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (var countryProperty in dataElement.EnumerateObject())
                    {
                        var nameOfCountry = countryProperty.Value.GetProperty("country").GetString();
                        countryNames.Add(nameOfCountry);
                    }
                }

                // Check if the provided country name is present in the list
                bool isPresent = countryNames.Contains(countryName);

                return isPresent;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        } 

        public string EnrollInACourse(Guid userId,Guid courseId, string countryName)
        {
                //1. validate the user and course
                //var user = _dbContext.Users.Find(userId);
                var user = _userRepository
                    .GetUserByUserIdAndIncludesEnrollmentsAndCourses(userId);


                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var course = _courseRepository
                    .GetCourseByCourseIdAndIncludesEnrollmentsAndUsers(courseId);

                if (course == null)
                {
                    throw new Exception("Course not found");
                }


                //check user is already enrolled
                //in the same course or not
                bool isAlreadyEnrolled = _enrollmentRepository
                    .IsAlreadyEnrolled(userId, courseId);

                if (isAlreadyEnrolled)
                {
                    throw new Exception($"{user.UserName} is already enrolled in {course.CourseName} course");
                }


            //check if country name is valid or not
            bool isValidCountry = IsPresentTheCountry(countryName);

            if (!isValidCountry)
            {
                throw new Exception("Country is not valid");
            }


                //2. generate new enrollment id
                var enrollment = new Enrollment();
                enrollment.EnrollmentId = Guid.NewGuid();

                //3. make the enrollment
                enrollment.Progress = Enums.Progress.Ongoing;
                enrollment.EnrollmentDate = DateTime.UtcNow;

                //4. in user's course list add the new course
                enrollment.Course = course;
                enrollment.CourseId = courseId;


                enrollment.User = user;
                enrollment.UserId = userId;

                user.Courses.Add(course);


                course.Users.Add(user);


                course.Enrollments.Add(enrollment);
                user.Enrollments.Add(enrollment);

            _enrollmentRepository.AddToEnrollmentDb(enrollment);

            _enrollmentRepository.Save();


            //_enrollmentRepository.AddToEnrollmentDbAsAsync(enrollment);
            //_enrollmentRepository.SaveAsAsync();



            return $"Your enrollment is successfull, enrollment id is {enrollment.EnrollmentId}";
            
           
        }


        public string UnEnroll(Guid enrollmentId)
        {

            // 1. Find the enrollment from the db
            //var enrollment = _dbContext.Enrollments
            //                            .Include(e => e.Course)
            //                                .ThenInclude(c => c.User)
            //                            .FirstOrDefault(e => e.EnrollmentId == enrollmentId);


            var enrollment = _enrollmentRepository
                .GetEnrollmentByEnrollmentIdAndIncludeTheUserTheCourseAndTheUsersofCourse
                (enrollmentId);

            // 2. Validate the enrollment
            if (enrollment == null)
            {
                throw new Exception("Enrollment not found");
            }

            // 3. Access the course and user from the enrollment
            var course = enrollment.Course;

            if (course == null)
            {
                throw new Exception("Course associated with enrollment is null");
            }

            var user = enrollment.User;

            if (user == null)
            {
                throw new Exception("User associated with course is null");
            }

            //var user = course.User;

            //var studentCourse = course.StudentCourses
               // .FirstOrDefault(e => e.CourseId == course.CourseId && e.UserId == user.UserId);

            //var user = _dbContext.Users.Find(studentCourse.UserId);

           

            try
            {
                // 4. Remove the course from the user's course list
                //user.Courses.Remove(course);

                //user.StudentCourses.Remove(studentCourse);
                //course.StudentCourses.Remove(studentCourse);

                // 5. Update references to null
                enrollment.Course = null;
                enrollment.User = null;

                //remove the list of enrollments in user and course table
                user.Enrollments.Remove(enrollment);
                course.Enrollments.Remove(enrollment);
                user.Courses.Remove(course);

              
                course.Users.Remove(user);


                // 6. Delete the enrollment
                //_dbContext.Enrollments.Remove(enrollment);
                _enrollmentRepository.Delete(enrollment);
                //_dbContext.StudentCourses.Remove(studentCourse);

                // 7. Save changes to the database
                //_dbContext.SaveChanges();
                _enrollmentRepository.Save();

                return "Successfully unenrolled the course";
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message
                return $"Error occurred: {ex.Message}";
            }
          
        }


        //public string DeleteEnrollment(Guid enrollmentId)
        //{
        //    var enrollment = _dbContext.Enrollments.Find(enrollmentId);

        //    if(enrollment == null)
        //    {
        //        return "Not Found";
        //    }

        //    _dbContext.Enrollments.Remove(enrollment);
        //    _dbContext.SaveChanges();

        //    return "Successfully deleted the enrollment";
        //}


        //List<Enrollment>GetAllTheEnrollments(Guid courseId)
        //{
        //    var course = _dbContext.Courses.Find(courseId);
        //    if (course == null)
        //    {
        //        throw new Exception("Course Not Found");
        //    }

        //    return course.Enrollments;
        //}

        public List<Enrollment>TrackProgress(Progress progress)
        {
            var list = _enrollmentRepository.TrackProgress(progress);

            return list;
        }
    }
}
