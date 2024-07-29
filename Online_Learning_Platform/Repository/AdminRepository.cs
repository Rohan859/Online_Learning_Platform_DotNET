using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    
    public class AdminRepository : IAdminRepository
    {
        private readonly AllTheDbContext _allTheDbContext;
        public AdminRepository(AllTheDbContext allTheDbContext)
        {
            _allTheDbContext = allTheDbContext;
        }
        public void AddToAdminDb(Admin admin)
        {
            _allTheDbContext.Admins.Add(admin);
        }

        public void Save()
        {
            _allTheDbContext.SaveChanges();
        }


        public Admin? FindAdminByEmail(string email)
        {
            Admin? admin = _allTheDbContext.Admins
                .FirstOrDefault(u => u.AdminEmail == email);

            return admin;
        }
    }
}
