using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.RepositoryInterface
{
    public interface IAdminRepository
    {
        public void AddToAdminDb(Admin admin);
        public void Save();
        public Admin? FindAdminByEmail(string email);
    }
}
