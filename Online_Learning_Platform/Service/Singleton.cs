﻿using Online_Learning_Platform.ServiceInterfaces;

namespace Online_Learning_Platform.Service
{
    public class Singleton : ISingleton
    {
        private Guid _id;

        public Singleton()
        {
            _id = Guid.NewGuid();
            Console.WriteLine(this.GetHashCode());
        }
        public Guid GetId()
        {
            Console.WriteLine(this.GetHashCode());
            return _id;
        }
    }
}
