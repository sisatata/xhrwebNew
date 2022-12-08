using ASL.AccessControl.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASL.AccessControl.Utility
{
    public class TokenBuilder
    {
        public static string BuildJwtBearerToken(AppUserAuthModel model, string tokenKey, string tokenIssuer)
        {
            var expirationTime = DateTime.Now.AddDays(1);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Email, model.UserName),
                    new Claim("UserId", model.UserId),
                    new Claim("CompanyId", model.CompanyId),
                    new Claim("EmployeeId", model.Id.ToString()),
                    new Claim("EmployeeCode", model.EmployeeId),
                    new Claim("EmployeeName", model.DisplayName)
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
