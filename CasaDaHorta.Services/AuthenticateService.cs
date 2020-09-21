using CasaDaHora.Domain.Amigo;
using CasaDaHorta.Repository.Amigo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CasaDaHorta.Services
{
    public class AuthenticateService
    {
        private AmigoRepository Repository { get; set; }

        private IConfiguration Configuration { get; set; }

        public AuthenticateService(AmigoRepository amigoRepository, IConfiguration configuration)
        {
            this.Repository = amigoRepository;
            this.Configuration = configuration;
        }

        public string AuthenticateAmigoDomain(string email, string password)
        {
            var amigoDomain = this.Repository.GetAmigoDomainByEmail(email);

            if (amigoDomain == null)
                return null;

            if (amigoDomain.Password != password)
                return null;

            return CreateToken(amigoDomain);
        }

        private string CreateToken(AmigoDomain amigoDomain)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, amigoDomain.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, amigoDomain.Nome));
            claims.Add(new Claim(ClaimTypes.Email, amigoDomain.Email));
            claims.Add(new Claim("password", amigoDomain.Password));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "AmigoDomain-API",
                Issuer = "AmigoDomain-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
