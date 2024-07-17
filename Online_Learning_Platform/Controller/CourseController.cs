using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs.ResponseDTO;
using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
       // private CourseService _courseService;
       private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        private ResponseDTO CreateCourseResponse(string message, string result)
        {
            var response = new ResponseDTO
            {
                Message = message,
                Result = result

            };
            return response;
        }


        [HttpPost("/addNewCourse")]
        public ActionResult<ResponseDTO> AddNewCourse([FromBody]Course course)
        {         
            try
            {
                string res = _courseService.AddNewCourse(course);

                var response = CreateCourseResponse("New Course Added", res);

                return Ok(response);               
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message});
            }
            
        }


        [HttpGet("/getAllAvailableCourses")]
        public ActionResult<CourseListResponseDTO> GetAllCourses()
        {
            var courseList = _courseService.GetAllCourses();

            var response = new CourseListResponseDTO
            {
                Message = "All the available courses",
                Courses = courseList
            };

            return Ok(response);
        }

        [HttpGet("/getCourseById")]
        public ActionResult<CourseResponseDTO> GetCourseByCourseId([FromQuery]Guid courseId)
        {
            try
            {
                var course = _courseService.GetCourseByCourseId(courseId);

                var response = new CourseResponseDTO
                {
                    Message = "Successfully got the course from the db",
                    Course = course
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});
            }
        }


        [HttpDelete("/deleteCourseById")]
        public ActionResult RemoveCourseById([FromQuery]Guid courseId)
        {

            try
            {
                Task res = _courseService.RemoveCourseById(courseId);

                //var response = CreateCourseResponse("Deleted the course", res);

                //return Ok(response);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
           
        }



        [HttpPut("/updateCourse")]
        public ActionResult<ResponseDTO> UpdateCourseDetails([FromBody]CourseDetailsUpdateDTO courseDetails)
        {
            try
            {
                string res = _courseService.UpdateCourseDetails(courseDetails);

                var response = CreateCourseResponse("Updated course", res);

                return Ok(response);
              
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
            
        }


        [HttpGet("/getNoOfReviewsByCourseId")]
        public ActionResult<ResponseDTO> GetNoOfReviewsByCourseId([FromQuery]Guid courseId)
        {
           try
            {
                var noOfReviews = _courseService.GetNoOfReviewsByCourseId(courseId);

                var response = CreateCourseResponse(
                    "Getting no of reviews for the course",
                    $"No of reviews are {noOfReviews}");

                return Ok(response);              
            }
            catch (Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
           
        }

        [HttpGet("/getNoOfEnrollmentsByCourseId")]
        public ActionResult<ResponseDTO> GetNoOfEnrollmentsByCourseId([FromQuery]Guid courseId)
        {
            try
            {
                var noOfEnrollments = _courseService
                    .GetNoOfEnrollmentsByCourseId(courseId);


                var response = CreateCourseResponse(
                    "Getting no of enrollments for the course",
                    $"No of enrollments are {noOfEnrollments}");

                return Ok(response);

                
            }
            catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

            
        }

        [HttpGet("/getListOfUserNameEnrolledByCourseId")]
        public ActionResult<FetchEnrollmentDTO> GetAllEnrollmentsByCourseId([FromQuery] Guid courseId)
        {      
            try
            {
                var ans = _courseService.GetAllEnrollmentsByCourseId(courseId);

                var response = new FetchEnrollmentDTO
                {
                    Message = "Fetching enrollments name",
                    Names = ans
                };

                return Ok(response);
                
            }
            catch (Exception e)
            {
                return NotFound(new {error = e.Message});
            }
           
        }
    }
}
