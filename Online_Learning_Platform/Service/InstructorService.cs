using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.Service
{
    public class InstructorService
    {
        private readonly AllTheDbContext _theDbContext;

        public InstructorService(AllTheDbContext context)
        {
            _theDbContext = context;
        }
        public string Register(Instructor instructor)
        {
            
            Instructor newInstructor = new Instructor
            {
                InstructorId = Guid.NewGuid(),
                InstructorName = instructor.InstructorName,
                Password = instructor.Password,
                Email = instructor.Email,
                Expertise = instructor.Expertise,
                MobileNo = instructor.MobileNo,
                Salary = instructor.Salary,
                Description = instructor.Description
            };

            _theDbContext.Instructors.Add(newInstructor);   
            _theDbContext.SaveChanges();

            return $"You have successfully registered and your id is {newInstructor.InstructorId}";
        }


        public string updateInstructor(InstructorUpdateRequestDTO instructorUpdateRequestDTO)
        {
            var instructor = _theDbContext.Instructors.Find(instructorUpdateRequestDTO.InstructorId);

            if (instructor == null)
            {
                return "Not Found";
            }

            var name=instructorUpdateRequestDTO.InstructorName;
            var email = instructorUpdateRequestDTO.Email;
            var password = instructorUpdateRequestDTO.Password;
            var expert = instructorUpdateRequestDTO.Expertise;
            var description = instructorUpdateRequestDTO.Description;

            if(name!=null)
            {
                instructor.InstructorName= name;
            }

            if(email!=null)
            {
                instructor.Email= email;
            }

            if(password!=null)
            {
                instructor.Password= password;
            }

            if(expert!=null)
            {
                instructor.Expertise= expert;
            }

            if (description != null)
            {
                instructor.Description= description;
            }

           
            _theDbContext.SaveChanges ();

            return "changes updated";
        }



        public string RemoveInstructor(Guid id)
        {
            var instructor = _theDbContext.Instructors.Find(id);

            if(instructor == null)
            {
                return "Not Found";
            }

            _theDbContext.Instructors.Remove(instructor);
            _theDbContext.SaveChanges();

            return "Instructor is deleted successfully";
        }


        public string AssignInstructor(Guid instructorId,Guid courseId)
        {
            var instructor = _theDbContext.Instructors.Find (instructorId);
            
            if( instructor == null )
            {
                return "Instructor not found";
            }

            var course =  _theDbContext.Courses.Find (courseId);

            if( course == null )
            {
                return "Course not found";
            }

            course.Instructors.Add (instructor);
           // instructor.Course= course;

           _theDbContext.Courses.Update (course);
            _theDbContext.SaveChanges();

            return $"{instructor.InstructorName} is assigned for {course.CourseName} course";
        }

        public int GetListOfInstructorByCourseId(Guid courseId)
        {
            var course = _theDbContext.Courses
                .Include(e => e.Instructors)
                .FirstOrDefault(e => e.CourseId == courseId);


            if (course == null )
            {
                throw new Exception("This course is not exist in our system");
            }
            return course.Instructors.Count();

        }
    }
}
