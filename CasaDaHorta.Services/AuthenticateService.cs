﻿using CasaDaHora.Domain.Account.Repository;
using CasaDaHora.Domain.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services
{
    public class AuthenticateService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IConfiguration _configuration;

        public AuthenticateService(IAccountRepository accountRepository, IConfiguration configurantion)
        {
            _accountRepository = accountRepository;
            _configuration = configurantion;
        }

        public string AuthenticateUser(string email, string password)
        {
            var account = _accountRepository.GetAccountByEmailPassword(email, password);

            if (account.Result == null)
            {
                return null;
            }

            return CreateToken(account);
        }

        private string CreateToken(Task<CasaDaHora.Domain.Account.Account> account)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, account.Result.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, account.Result.Email));
            claims.Add(new Claim(ClaimTypes.Name, account.Result.Nome));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Audience = "CASADAHORTA-API",
                Issuer = "CASADAHORTA-API"
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

    }
}
