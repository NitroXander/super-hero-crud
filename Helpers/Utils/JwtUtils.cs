using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using SuperHeros.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeros.Helpers.Utils
{
    public static class JwtUtils
    {
        static string secretKey = "MySuP3R$3Cr3TK3y!MySuP3R$3Cr3TK3y!";

        public static string GenerateJwtToken(UserModel user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            List<Claim> claims = new List<Claim>
            {
                new Claim("user_id", user.id.ToString()),
                new Claim("username", user.first_name+ " " +user.last_name),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
