using Online_Learning_Platform.ServiceInterfaces;

namespace Online_Learning_Platform.Service
{
    public class Transient : ITransient
    {
        private Guid _id;

        public Transient()
        {
            _id = Guid.NewGuid();
        }
        public Guid GetId()
        {
            return _id;
        }
    }
}
