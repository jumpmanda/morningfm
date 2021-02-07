using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MorningFM
{
    public interface IAuthenticationService
    {
        string Authorize(Guid user); 
    }

    public class AuthenticationService: IAuthenticationService
    {
        private readonly IConfiguration _configuration; 
        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(IConfiguration)} not provided."); 
        }

        public string Authorize(Guid user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetSection("JwtConfig").GetSection("secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
