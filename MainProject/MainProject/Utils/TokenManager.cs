using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MainProject.Utils
{
    public class TokenManager
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI3cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        public static string GenerateToken(UsersRegister UsersBO)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            int TokenExpiry = 720;
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = "MyProjectName",
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("USER_ID", UsersBO.USER_ID.ToString()),
                        new Claim("USER_ROLE", UsersBO.USER_ROLE.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpiry),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
}