using Microsoft.IdentityModel.Tokens;
using Online_Learning_Platform.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Online_Learning_Platform.Service
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IConfiguration configuration)
        {
            _issuer = configuration["JWT:ValidIssuer"]!;
            _secretKey = configuration["JWT:Secret"]!;
            _audience = configuration["JWT:ValidAudience"]!;
        }


        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            
            var token = new JwtSecurityToken
                (
                    issuer:_issuer,
                    audience: _audience,
                    claims:claims,
                    expires:DateTime.Now.AddMinutes(15),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int GetId()
        {
            return this.GetHashCode();
        }
    }
}
