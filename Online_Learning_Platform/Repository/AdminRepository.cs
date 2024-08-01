using Microsoft.EntityFrameworkCore;
using Online_Learning_Platform.AllDbContext;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;

namespace Online_Learning_Platform.Repository
{
    
    public class AdminRepository : IAdminRepository
    {
        //private readonly AllTheDbContext _allTheDbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public AdminRepository(IServiceScopeFactory serviceScopeFactory)
        {
            //_allTheDbContext = allTheDbContext;
            _serviceScopeFactory = serviceScopeFactory;
        }
        private AllTheDbContext CreateDbContext()
        {
            var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<AllTheDbContext>();
        }

       
        public void AddToAdminDb(Admin admin)
        {
            var _allTheDbContext = CreateDbContext();
            _allTheDbContext.Admins.Add(admin);
            _allTheDbContext.SaveChanges();
        }

        public void Save()
        {
            var _allTheDbContext = CreateDbContext();
            _allTheDbContext.SaveChanges();
        }


        public Admin? FindAdminByEmail(string email)
        {
            var _allTheDbContext = CreateDbContext();
            Admin? admin = _allTheDbContext.Admins
                .FirstOrDefault(u => u.AdminEmail == email);

            return admin;
        }
        public Admin? FindAdminById(Guid adminId)
        {
            var _allTheDbContext = CreateDbContext();

            Admin? admin = _allTheDbContext.Admins
                .FirstOrDefault(x => x.AdminId == adminId);

            return admin;
        }
        public string DeleteAdminById(Guid adminId)
        {
            var _allTheDbContext = CreateDbContext();

            Admin? admin = FindAdminById(adminId);

            if(admin != null)
            {
                _allTheDbContext.Admins.Remove(admin);
                _allTheDbContext.SaveChanges();
                return "Admin is deleted successfully";
            }

            return "Admin does not exist";
        }
    }
}
