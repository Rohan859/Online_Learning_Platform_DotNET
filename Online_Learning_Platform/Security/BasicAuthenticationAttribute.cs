using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Online_Learning_Platform.Security
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        private const string Realm = "MyRealm";

        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string authHeader = filterContext.HttpContext.Request.Headers["Authorization"]!;

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Extract credentials
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int separatorIndex = usernamePassword.IndexOf(':');
                string username = usernamePassword.Substring(0, separatorIndex);
                string password = usernamePassword.Substring(separatorIndex + 1);

                // Validate credentials (replace with your own authentication logic)
                if (IsValidUser(username, password))
                {
                    return; // Authorized
                }
            }

            // Not authorized, send authentication challenge
            filterContext.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
            filterContext.Result = new UnauthorizedResult();
        }

       

        private bool IsValidUser(string username, string password)
        {
            // Replace with your authentication logic (e.g., check against database)
            return username == "rohan" && password == "1234";
        }
    }
}
