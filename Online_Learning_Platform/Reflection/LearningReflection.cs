using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using System.Collections.Concurrent;
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

            object? serviceInstance = _serviceProvider.GetService(type);


            if (serviceInstance == null)
            {
                // If service is not available, create it manually
                ConstructorInfo? ctor = type.GetConstructors()
                    .FirstOrDefault(c => c.GetParameters()
                    .All(p => _serviceProvider.GetService(p.ParameterType) != null));

                if (ctor == null)
                {
                    throw new Exception($"Suitable constructor not found for {serviceClassName}");
                }

                // Resolve constructor parameters from DI container
                object[] parameters = ctor.GetParameters()
                    .Select(p => _serviceProvider.GetRequiredService(p.ParameterType))
                    .ToArray();

                serviceInstance = ctor.Invoke(parameters);
            }

            MethodInfo? methodInfo = type.GetMethod(methodName);

            if (methodInfo == null)
            {
                throw new Exception($"{methodName} is not found in {serviceClassName} class");
            }

            methodInfo.Invoke(serviceInstance, new object[] {"rohan@gmail.com"});
            Console.WriteLine(serviceInstance.GetHashCode());
        }
    }
}
