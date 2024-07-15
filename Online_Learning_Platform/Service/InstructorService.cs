using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Enums;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.Validation;
using System.Text;

namespace Online_Learning_Platform.Service
{
    public class InstructorService : IInstructorService
    {
        private readonly IMapper _mapper;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICourseRepository _courseRepository;

        public InstructorService(
            IMapper mapper,
            IInstructorRepository instructorRepository,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
        }
        public string Register(Instructor instructor)
        {
            var validator = new InstructorValidator();
            var res = validator.Validate(instructor);

            StringBuilder sb = new StringBuilder();

            if (!res.IsValid)
            {
                foreach (var failure in res.Errors)
                {
                    Console.WriteLine(failure.ErrorMessage);
                    sb.AppendLine(failure.ErrorMessage);
                }
                throw new Exception(sb.ToString());
            }

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

            //_theDbContext.Instructors.Add(newInstructor);   
            //_theDbContext.SaveChanges();
            _instructorRepository.SaveToInstructorDb(newInstructor);
            _instructorRepository.Save();

            return $"You have successfully registered and your id is {newInstructor.InstructorId}";
        }


        public string updateInstructor(InstructorUpdateRequestDTO instructorUpdateRequestDTO)
        {
            var instructor = _instructorRepository
                .FindInstructorById(instructorUpdateRequestDTO.InstructorId);

            if (instructor == null)
            {
                return "Not Found";
            }

            _mapper.Map(instructorUpdateRequestDTO, instructor);
           
            _instructorRepository.Save();

            return "changes updated";
        }



        public string RemoveInstructor(Guid id)
        {
            var instructor = _instructorRepository
                .FindInstructorByIdAndIncludeCourse(id);

            if (instructor == null)
            {
                return "Not Found";
            }

            if(instructor.Course == null || instructor.Course.Instructors.Count == 0)
            {
                //just delete the instructor 
                //there is no course assigned to this instructor
                _instructorRepository.DeleteInstructor(instructor);
                _instructorRepository.Save();

                return "Instructor is deleted successfully";
            }

            //now inside the course
            //there is some instructor present
            //in instructor list
            //so remove the instructor from the instructor list
            instructor.Course.Instructors.Remove(instructor);
            _instructorRepository.DeleteInstructor(instructor);
            _instructorRepository.Save();

            return "Instructor is deleted successfully";
            
           
        }


        public string AssignInstructor(Guid instructorId,Guid courseId)
        {
            var instructor = _instructorRepository.FindInstructorById(instructorId);
            
            if(instructor == null)
            {
                return "Instructor not found";
            }

            var course = _courseRepository.FindCourseById(courseId);

            if(course == null)
            {
                return "Course not found";
            }

            course.Instructors.Add (instructor);
           // instructor.Course= course;

           //_theDbContext.Courses.Update (course);
           _instructorRepository.Save();

            return $"{instructor.InstructorName} is assigned for {course.CourseName} course";
        }

        public int GetCountOfInstructorByCourseId(Guid courseId)
        {
            var course =_courseRepository
                .FindCourseByIdAndIncludeInstructors(courseId);


            if (course == null )
            {
                throw new Exception("This course is not exist in our system");
            }
            return course.Instructors.Count();

        }


        

        public List<Instructor>GetListOfInstructorsByCourseId(Guid courseId)
        {
            List<Instructor>instructorList = _instructorRepository
                .FindListOfInstructorsByCourseId(courseId);

            return instructorList; 
        }
    }
}
