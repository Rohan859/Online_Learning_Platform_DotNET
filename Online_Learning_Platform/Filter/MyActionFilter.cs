using Microsoft.AspNetCore.Mvc.Filters;

namespace Online_Learning_Platform.Filter
{
    public class MyActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("I have executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("I am executing");
        }
    }
}
