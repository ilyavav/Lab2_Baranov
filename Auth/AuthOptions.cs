using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab2_Baranov.Auth
{
    internal class AuthOptions
    {
        public static string Issuer => "Lab2_Baranov";

        public static string Audience => "APIClients";

        public static int LifetimeInYears => 1;

        public static SecurityKey SigningKey =>
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("superSecretKeyForLab2Baranov2026_123456"));

        public static object GenerateToken(bool isAdmin = false)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
                new Claim(
                    ClaimsIdentity.DefaultRoleClaimType,
                    isAdmin ? "admin" : "guest")
            };

            var identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                notBefore: now,
                expires: now.AddYears(LifetimeInYears),
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(
                    SigningKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new
            {
                token = encodedJwt
            };
        }
    }
}