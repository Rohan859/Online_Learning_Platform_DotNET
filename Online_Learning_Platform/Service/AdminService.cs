﻿using Online_Learning_Platform.DTOs.ResuestDTO;
using Online_Learning_Platform.Model;
using Online_Learning_Platform.RepositoryInterface;
using Online_Learning_Platform.ServiceInterfaces;

namespace Online_Learning_Platform.Service
{
    
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public string RegisterAdmin(AdminRegisterRequestDTO adminRegisterRequestDTO)
        {
            Admin admin = new Admin();
            admin.AdminName = adminRegisterRequestDTO.AdminName;
            admin.AdminEmail = adminRegisterRequestDTO.AdminEmail;
            admin.AdminPassword = adminRegisterRequestDTO.AdminPassword;  

            _adminRepository.AddToAdminDb(admin);
            _adminRepository.Save();

            return admin.AdminName + " is registered as admin with id " + admin.AdminId;
        }

        public Admin? FindAdminByEmail(string email)
        {
            Admin? admin = _adminRepository.FindAdminByEmail(email);

            return admin;
        }

        public string DeleteAdminById(Guid adminId)
        {
            string res = _adminRepository.DeleteAdminById(adminId);

            if(res == "Admin does not exist")
            {
                throw new Exception(res);
            }
            return res;
        }
    }
}
