using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Platform.DTOs
{
    public class UserProfileUpdateRequestDTO
    {
        public Guid UserId { get; set; }

        [StringLength(40)]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
    }
}
