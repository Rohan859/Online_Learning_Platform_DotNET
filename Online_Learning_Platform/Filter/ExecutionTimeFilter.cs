using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Online_Learning_Platform.Filter
{
    public class ExecutionTimeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();

            var resultContext = await next();

            stopWatch.Stop();

            var executionTime = stopWatch.ElapsedMilliseconds;

            Console.WriteLine($"Action executed in {executionTime} ms");
        }


    }
}
