using Online_Learning_Platform.Interfaces;
using Online_Learning_Platform.Service;
using System.Reflection;

namespace Online_Learning_Platform.Reflection
{
    public class DemoClass
    {
        private readonly object _serviceInstance;

        public DemoClass(object serviceInstance)
        {
            _serviceInstance = serviceInstance;
        }

        public void Display()
        {
            Console.WriteLine("Rohan is learning reflection in C#, and calling me by the help of reflection");

            // Assuming the serviceInstance has a method named ServiceMethod
            MethodInfo serviceMethod = _serviceInstance

                .GetType()
                .GetMethod("ServiceMethod")!;

            if (serviceMethod == null)
            {
                throw new ArgumentException("ServiceMethod not found in the service instance.");
            }

            // Invoke ServiceMethod on the serviceInstance
            serviceMethod.Invoke(_serviceInstance, null);
        }
    }
}
