

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Online_Learning_Platform.Filter
{
    public class AuthorizationFilter : Attribute,IAuthorizationFilter
    {
        private static readonly HashSet<string> NoAuthPaths = new HashSet<string>
        {
         //   "/",
            "/adminLogin",
            "/signUpAdmin",
            "/userRegister",
            "/userLogin",
            "/instructorRegister",
            "/instructorLogin",
            "/reflect",
            "/singleton",
            "/jwtIdAsSingleton",
            "/getIdAsSingletonFromAdminService",
            "/fetch",
            "/hi"
        };


        private static readonly Dictionary<string, string[]> requiredRoles = new Dictionary<string, string[]>()
            {
                //for Admin Controller
                {"/deleteAdminById", new[]{"Admin"} },
               


                //for User Controller
                {"/deleteUser", new[]{"Admin"} },
                {"/updateUser", new[]{"User"} },
                {"/getCourseListByUser", new[]{"User", "Admin" } },
                {"/countEnrollCoursesByUserId", new[]{"User"} },
                {"/getNoOfReviewsByUserId", new[]{"User"} },
                {"/getUserById", new[]{"Admin"} },



                //for Course Controller
                {"/addNewCourse", new[]{"Admin"} },
                {"/getAllAvailableCourses", new[]{"Admin","User"} },
                {"/getCourseById", new[]{"Admin","User"} },
                {"/deleteCourseById", new[]{"Admin"} },
                {"/updateCourse", new[]{"Admin"} },
                {"/getNoOfReviewsByCourseId", new[]{"Admin"} },
                {"/getNoOfEnrollmentsByCourseId", new[]{"Admin"} },
                {"/getListOfUserNameEnrolledByCourseId", new[]{"Admin"} },



                //for Instructor Controller
                {"/updateInstructor", new[]{ "Tutor" } },
                {"/deleteInstructor", new[]{ "Admin" } },
                {"/assignInstructorByCourseId", new[]{ "Admin" } },
                {"/noOfInstructorsByCourseId", new[]{ "Admin" } },
                {"/getListOfInstructorsByCourseId", new[]{ "Admin" } },



                //for Enrollment Controller
                {"/enroll", new[]{ "User" } },
                {"/unenroll", new[]{ "Admin" } },



                //for Review Controller
                {"/submitReview", new[]{ "User" } },
                {"/updateReview", new[]{ "User" } },
                {"/deleteReview", new[]{ "User" } },



                //for Course Analytics Controller
                {"/getRevenueByCourseId", new[]{ "Admin" } },
                {"/countProgress", new[]{ "Admin" } }

            };
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var path = context.HttpContext.Request.Path;

            if (NoAuthPaths.Contains(path))
            {
                return;
            }

            var authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader) || authHeader == "No Auth")
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            if(!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            if (requiredRoles.TryGetValue(path, out var roles))
            {
                if(!roles.Any(role => context.HttpContext.User.IsInRole(role)))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
