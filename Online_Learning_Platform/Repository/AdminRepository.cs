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
    }
}
