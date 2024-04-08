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

        public static bool ValidateJwtToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secretKey);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

                long userId = long.Parse(jwtToken.Claims.First(x => x.Type == "user_id").Value);
            
                using(ApplicationDbContext context = new ApplicationDbContext())
                {
                    UserModel? user = context.Users.FirstOrDefault(u => u.id == userId);

                    if(user == null)
                    {
                        return false;
                    }
                    else
                    {
                        LoginDetailsModel loginDetails =  context.LoginDetails.Where(details => details.id == userId).First();

                        if(loginDetails.jwt_token != null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
