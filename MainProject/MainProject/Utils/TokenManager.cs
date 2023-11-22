using DAL.Models;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using MainProject.Models;

namespace MainProject.Utils
{
    public class TokenManager
    {
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI3cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        public static string GenerateToken(UsersRegister Users)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            int TokenExpiry = 720;
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = "MyProjectName",
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("UserID", Users.UserID.ToString()),
                        new Claim("Roll", Users.Roll.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpiry),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static Ent_UserRegistration ValidateToken(string token)
        {
            ClaimsPrincipal principal = GetPrincipal(token);
            return GetUserFromPrincipal(principal);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Ent_UserRegistration GetUserFromPrincipal(ClaimsPrincipal principal)
        {
            if (principal == null) return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim userpidClaim = identity.FindFirst("UserID");
            Claim usertypeClaim = identity.FindFirst("Roll");
            Ent_UserRegistration usersDTO = new Ent_UserRegistration
            {
                id = int.Parse(userpidClaim.Value),
                roll = usertypeClaim.Value
            };
            return usersDTO;
        }
    }
}