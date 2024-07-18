using System.Reflection;

namespace Online_Learning_Platform.Reflection
{
    public class LearningReflection
    {
        public void InvokeMethod(string className,string methodName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            //getting the type by class name
            Type type = assembly.GetType(className)!;

            if (type != null)
            {
                MethodInfo method = type.GetMethod(methodName)!;

                if (method != null)
                {
                    //first create the instance of the class
                    object obj = Activator.CreateInstance(type)!;

                    //call the method
                    method.Invoke(obj, null);//null because there is no parameter
                }
                else
                {
                   throw new Exception($"{methodName} is not found in the class {className}");
                }
            }
            else
            {
                throw new Exception($"{className} is not found in current assembly");
            }
        }
    }
}
