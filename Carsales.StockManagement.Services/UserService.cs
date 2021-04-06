using Carsales.StockManagement.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Carsales.StockManagement.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationSettings _settings;
        private readonly List<UserModel> users;
        public UserService(IOptions<AuthenticationSettings> settings)
        {
            _settings = settings.Value;
            users = new List<UserModel>() {
            new UserModel(){ UserName="Hossein1" , Password="123", DealerId= Guid.NewGuid() },
            new UserModel(){ UserName="Hossein2" , Password="1234", DealerId= Guid.NewGuid() }
            };
        }
        public string GetToken(AuthenticationModel authenticationModel)
        {
            var user = users.FirstOrDefault(u => u.UserName == authenticationModel.Username && u.Password==authenticationModel.Password);
            if (user == null)
                return null;
            return $"Bearer {GenerateJSONWebToken(authenticationModel.Username, user.DealerId.ToString())}";
        }
        private string GenerateJSONWebToken(string Username, string DealerId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Username),
                new Claim(CustomClaimTypes.DealerId, DealerId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _settings.JwtIssuer,
                audience: _settings.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
