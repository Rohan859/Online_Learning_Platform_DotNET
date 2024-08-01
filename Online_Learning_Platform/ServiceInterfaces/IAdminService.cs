using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;

namespace Online_Learning_Platform.ServiceInterfaces
{
    public interface IAdminService
    {
        public string RegisterAdmin(AdminRegisterRequestDTO adminRegisterRequestDTO);
        public Admin? FindAdminByEmail(string email);
        public string DeleteAdminById(Guid adminId);
    }
}
