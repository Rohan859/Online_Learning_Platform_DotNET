using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using System.Reflection;

namespace Online_Learning_Platform.Reflection
{
    public class LearningReflection
    {
        private readonly Assembly _assembly;
       // private readonly ICourseService _courseService;
       private readonly IServiceProvider _serviceProvider;
        
        public LearningReflection(IServiceProvider serviceProvider)
        {
            _assembly = Assembly.GetExecutingAssembly();
            //_courseService = courseService;
            _serviceProvider = serviceProvider;
        }
        public void InvokeMethod(
            string serviceClassName,
            string methodName
            )
        {
           Type type = _assembly.GetType(serviceClassName)!;

            if (type == null)
            {
                throw new Exception($"{serviceClassName} is not found");
            }

            // Get constructor info
            ConstructorInfo? ctor = type.GetConstructor(new Type[] {
                typeof(IMapper),
                typeof(IEnrollmentService),
                typeof(IInstructorService),
                typeof(IReviewService),
                typeof(ICourseRepository),
                typeof(IHttpClientFactory)
            });


            if (ctor == null)
            {
                throw new Exception($"Constructor with required dependencies not found for {serviceClassName}");
            }


            // constructor parameters from DI container
            object[] parameters = {
                _serviceProvider.GetRequiredService<IMapper>(),
                _serviceProvider.GetRequiredService<IEnrollmentService>(),
                _serviceProvider.GetRequiredService<IInstructorService>(),
                _serviceProvider.GetRequiredService<IReviewService>(),
                _serviceProvider.GetRequiredService<ICourseRepository>(),
                _serviceProvider.GetRequiredService<IHttpClientFactory>()
            };

            //object obj = Activator.CreateInstance(type)!;
            //object? serviceInstance = Activator.CreateInstance(type);
            object serviceInstance = ctor.Invoke(parameters);

            if (serviceInstance == null)
            {
                throw new Exception("Failed to create instance of service class by dependency injection");
            }

            MethodInfo? methodInfo = type.GetMethod(methodName);

            if (methodInfo == null)
            {
                throw new Exception($"{methodName} is not found in {serviceClassName} class");
            }

            methodInfo.Invoke(serviceInstance,null);
        }
    }
}
