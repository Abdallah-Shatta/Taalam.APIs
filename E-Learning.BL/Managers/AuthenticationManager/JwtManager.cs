using E_Learning.BL.DTO.User;
using E_Learning.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.AuthenticationManager
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;

        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AuthenticationResponseDTO createJwtToken(User user)
        {
            // Create a DateTime object representing the token expiration time by adding the number of minutes specified in the configuration to the current UTC time.
            DateTime expiration = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:ExpiryMinutes"]));

            // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
            var claims = new List<Claim>
        {
           new Claim(JwtRegisteredClaimNames.Sub , user.FName),
           new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
           new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
       };


            // Create a SymmetricSecurityKey object using the key specified in the configuration.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("2846ae81-0119-49c8-a6ad-21f693a61ad3-7e85d41a-2627-4f8b-bfe0-30de8eb81fd5"));

            // Create a SigningCredentials object with the security key and the HMACSHA256 algorithm.
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a JwtSecurityToken object with the given issuer, audience, claims, expiration, and signing credentials.
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
           issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: expiration,
            signingCredentials: signingCredentials,
            claims : claims
       
      
            );

            // Create a JwtSecurityTokenHandler object and use it to write the token as a string.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            // Create and return an AuthenticationResponse object containing the token, user email, user name, and token expiration time.
            return new AuthenticationResponseDTO() { Token = token, Email = user.Email, FName = user.FName, Expiration = expiration };
        }
    }
}
