using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskMgmt.Services.Helpers
{
    public class JwtHelper
    {
        // private readonly string _secret;
        // private readonly string _issuer;
        // private readonly string _audience;

        // public JwtHelper(string secret, string issuer, string audience)
        // {
        //     _secret = secret;
        //     _issuer = issuer;
        //     _audience = audience;
        // }

        public string GenerateToken(int userId)
        {
            var claims = new[] { new Claim("UserId", userId.ToString()) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdfasdfasdfasdfasdfzxcvq2qweqweoiuoixcuzvoizxucoizuxciouzxicouzixcu"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}