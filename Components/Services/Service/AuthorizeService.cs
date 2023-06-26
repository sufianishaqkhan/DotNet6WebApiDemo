using DataAccess.Interface;
using Entities.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IConfiguration _configuration;
        public AuthorizeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AuthenticateUser(string pUserName, string pPassword)
        {
            var isDevEnv = _configuration["Is_Dev_Environment"];
            if (isDevEnv != null && isDevEnv.ToString().ToLower() == "true")
            {
                return true;
            }

            var EncryptedPassword = _configuration["Api_Password"];
            var Password = _configuration["Api_Password"];
            var Username = _configuration["Api_UserName"];

            if ((pUserName == Username) && (Password == EncryptedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<AuthenticationModel> CreateToken(string userName)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(userName);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var response = new AuthenticationModel
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                expires_in = _configuration["Jwt:API_TOKEN_EXPIRY"],
                token_type = "Bearer"
            };

            return response;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["Jwt:Key"];
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:API_TOKEN_EXPIRY"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return token;
        }
        private async Task<List<Claim>> GetClaims(string userName)
        {
            var claims = new List<Claim>()
            {
                new Claim("username" ,userName),
            };
            return claims;
        }
    }
}
