using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Pub.API.Service.Authentication
{
    internal class AuthenticationService
    {
        private readonly IConfiguration configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateJSONWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,username),
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateJSONWebToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = AuthenticationParameters.GetTokenValidationParameters(configuration);

            SecurityToken validatedToken;

            try {
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return true;
            }catch(Exception) { return false; }
            
        }
    }
}
