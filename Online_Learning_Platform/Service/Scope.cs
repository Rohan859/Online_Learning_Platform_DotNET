using Online_Learning_Platform.ServiceInterfaces;

namespace Online_Learning_Platform.Service
{
    public class Scope : IScoped
    {
        private Guid _id;

        public Scope()
        {
            _id = Guid.NewGuid();
        }
        public Guid GetId()
        {
            return _id;
        }
    }
}
