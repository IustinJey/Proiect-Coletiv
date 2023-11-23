using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using skillz_backend.models;

namespace skillz_backend.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly SymmetricSecurityKey _key;

        public AuthenticationService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            // Poți adăuga și alte claim-uri necesare pentru token
        };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidatePassword(string enteredPassword, string storedPasswordHash, byte[] salt)
        {
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

            using (var hmac = new System.Security.Cryptography.HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(enteredPasswordBytes);
                return computedHash.SequenceEqual(Convert.FromBase64String(storedPasswordHash));
            }
        }

    }
}