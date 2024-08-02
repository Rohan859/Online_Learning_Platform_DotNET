using System.Security.Claims;

namespace Online_Learning_Platform.ServiceInterfaces
{
    public interface IJwtService
    {
        public string GenerateToken(IEnumerable<Claim> claims);
        public int GetId();
        
    }
}
