﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.DTOs;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.Service;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpPost("/addNewCourse")]
        public ActionResult<string> AddNewCourse([FromBody]Course course)
        {
            string res=_courseService.AddNewCourse(course);

            if(res == "Not Possible")
            {
                return BadRequest("please enter all the details about the course");
            }

            return Ok(res);
        }


        [HttpGet("/getAllAvailableCourses")]
        public ActionResult<List<Course>>GetAllCourses()
        {
            var courseList = _courseService.GetAllCourses();
            return Ok(courseList);
        }

        [HttpGet("/getCourseById")]
        public ActionResult<Course> GetCourseByCourseId([FromQuery]Guid courseId)
        {
            var course = _courseService.GetCourseByCourseId(courseId);

            if(course == null)
            {
                return NotFound($"this id {courseId} does not exist in our system");
            }

            return Ok(course);
        }


        [HttpDelete("/deleteCourseById")]
        public ActionResult<string>RemoveCourseById([FromQuery]Guid courseId)
        {
            string res = _courseService.RemoveCourseById(courseId);

            if( res == "Not Found")
            {
                return NotFound("This course is not exist in the system");
            }

            return Ok(res);
        }



        [HttpPut("/updateCourse")]
        public ActionResult<string> UpdateCourseDetails([FromBody]CourseDetailsUpdateDTO courseDetails)
        {
            string res=_courseService.UpdateCourseDetails(courseDetails);

            if(res== "Not Found")
            {
                return NotFound("Course does not exist in our system");
            }

            return Ok(res);
        }


        [HttpGet("/getNoOfReviewsByCourseId")]
        public ActionResult<string> GetNoOfReviewsByCourseId([FromQuery]Guid courseId)
        {
           try
            {
                var noOfReviews = _courseService.GetNoOfReviewsByCourseId(courseId);
                return Ok($"No of reviews are : {noOfReviews}");
            }
            catch (Exception e)
            {
                Console.WriteLine("the error is "+e.Message);
            }
            return BadRequest("Course is not exist");
        }

        [HttpGet("/getNoOfEnrollmentsByCourseId")]
        public ActionResult<string> GetNoOfEnrollmentsByCourseId([FromQuery]Guid courseId)
        {
            try
            {
                var noOfEnrollments = _courseService.GetNoOfEnrollmentsByCourseId(courseId);
                return Ok($"The no of enrollments for this course are : {noOfEnrollments}");
            }
            catch(Exception e)
            {
                Console.WriteLine("The error is "+e.Message);
            }

            return BadRequest("Course does not exist");
        }

        [HttpGet("/getListOfUserNameEnrolledByCourseId")]
        public ActionResult<List<string>> GetAllEnrollmentsByCourseId([FromQuery] Guid courseId)
        {
            try
            {
                var ans = _courseService.GetAllEnrollmentsByCourseId(courseId);

                return Ok(ans);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is : " + e.Message);
            }

            return BadRequest("Course does not exist");
        }
    }
}
