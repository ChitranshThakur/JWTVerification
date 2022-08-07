using JWTVerification.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTVerification.Repository
{
    public class JWTManagerRepository : IJWtManagerRepository
    {
        private readonly IConfiguration _configuration;
        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        Dictionary<string, string> userRecords = new Dictionary<string, string> {

            {"Chitransh","16chit$H" },
            {"Vishu","26vi$Hu"},
            {"Anita","03aniT@"},
        };

        public Tokens Authenticate(Users users)
        {
            if (!userRecords.Any(x => x.Key == users.Name && x.Value == users.Password)) 
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokendescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, users.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokendescriptior);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }


    }
}
