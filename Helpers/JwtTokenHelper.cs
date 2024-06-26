using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingControlPanel.Models;
using Microsoft.IdentityModel.Tokens;

namespace BankingControlPanel.Helpers
{
    public static class JwtTokenHelper
    {
        public static string GenerateJwtToken(ApplicationUser user, IConfiguration configuration, IList<string> roles)
        {
            // Create a list of claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!), // Subject claim with user's email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
                new Claim(ClaimTypes.NameIdentifier, user.Id), // Name identifier claim with user's ID
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create a symmetric security key from the configuration's JWT key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            // Create signing credentials using the key and HmacSha256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"], // Issuer from configuration
                audience: configuration["Jwt:Audience"], // Audience from configuration
                claims: claims, // List of claims
                expires: DateTime.UtcNow.AddHours(1), // Expiration time (adjust as needed)
                signingCredentials: creds // Signing credentials
            );

            // Return the serialized JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
