using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using CasaDaHorta.Repository.Context;
using CasaDaHorta.Services.AmigoServices.AjudaPadrão;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.AmigoServices
{
    public class AmigoServices : IAmigoServices
    {
        private CasaDaHortaContext Context { get; set; }
        private IConfiguration Configuration { get; set; }

        public IAmigoRepository AmigoRepository { get; set; }
        public AmigoServices(IAmigoRepository amigoRepository)
        {
            AmigoRepository = amigoRepository;
        }

        public async Task<AmigoDomainResponse> CreateAmigoDomain(Guid Id, string Nome, string Sobrenome, string email, string password, string UrlFoto)
        {
            AmigoDomain amigoDomain = new AmigoDomain
            {
                Id = Id,
                Nome = Nome,
                Sobrenome = Sobrenome,
                Email = email,
                Password = password,
                UrlFoto = UrlFoto,
            };

            await AmigoRepository.CreateAmigo(amigoDomain);
            AmigoDomainResponse amigoDomainResponse = Ajuda.ConvertendoAmigoDomainEmAmigoDomainResponse(amigoDomain);
            return amigoDomainResponse;
        }
        public void Save(AmigoDomain amigoDomain)
        {
            if (this.GetAmigoDomainByEmail(amigoDomain.Email) != null)
            {
                throw new Exception("Já existe um aluno com este email, por favor cadastre outro email");
            }

            var anoAtual = DateTime.Now.Date.Year;

            if ((anoAtual - Convert.ToDateTime(amigoDomain.Datanascimento).Date.Year) < 21)
            {
                throw new Exception("Para cadastrar um novo aluno ele deve no minimo 21 anos");
            }

            this.AmigoRepository.CreateAmigo(amigoDomain);
        }

        public async Task<AmigoDomain> GetAmigoDomainByEmail(string email)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetByEmail(email);
            return amigoDomain;
        }

        public async Task<AmigoDomainResponse> GetByEmail(string email)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetByEmail(email);
            AmigoDomainResponse amigoDomainResponse = Ajuda.ConvertendoAmigoDomainEmAmigoDomainResponse(amigoDomain);
            return amigoDomainResponse;
        }

        public async Task<AmigoDomain> GetAmigoDomainById(Guid id)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetById(id);
            return amigoDomain;
        }

        public async Task<AmigoDomainResponse> GetById(Guid id)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetById(id);
            AmigoDomainResponse amigoDomainResponse = Ajuda.ConvertendoAmigoDomainEmAmigoDomainResponse(amigoDomain);
            return amigoDomainResponse;
        }

        public async Task<List<AmigoDomainResponse>> GetAll()
        {
            List<AmigoDomain> amigoDomains = await AmigoRepository.GetAll();
            List<AmigoDomainResponse> amigoDomainResponses = new List<AmigoDomainResponse>();

            foreach (AmigoDomain t in amigoDomains)
            {
                amigoDomainResponses.Add(new AmigoDomainResponse
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Sobrenome = t.Sobrenome,
                    Email = t.Email,
                    UrlFoto = t.UrlFoto,
                });
            }

            return amigoDomainResponses;
        }

        public async Task<bool> UpdateAmigoDomain(Guid id, string Nome, string Sobrenome, string Email, string Password, string UrlFoto)
        {
            var amigoDomain = await AmigoRepository.GetById(id);
            if (!string.IsNullOrEmpty(Nome))
            {
                amigoDomain.Nome = Nome;
            }
            if (!string.IsNullOrEmpty(Sobrenome))
            {
                amigoDomain.Sobrenome = Sobrenome;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                amigoDomain.Email = Email;
            }
            if (!string.IsNullOrEmpty(Password))
            {
                amigoDomain.Password = Password;
            }
            if (!string.IsNullOrEmpty(UrlFoto))
            {
                amigoDomain.Password = Password;
            }

            AmigoRepository.UpdateAmigo(amigoDomain);
            return true;
        }

        public async Task<List<AmigoSeguidorResponse>> GetSeguidores(Guid userId)
        {
            try
            {
                AmigoDomain amigoDomain = Ajuda.ConvertendoAmigoDomainResponseEmAmigoDomain(await GetById(userId));

                if (amigoDomain == null)
                    return null;

                List<AmigoSeguidorResponse> amigosSeguidores = await AmigoRepository.GetAmigoDosAmigos(amigoDomain);
                return amigosSeguidores;
            }
            catch (Exception e)
            {
               return null;
            }
        }

        public async Task<bool> AddSeguidores(Guid amigoDomainId, Guid amigosSeguidoresId)
        {
            if (amigoDomainId == amigosSeguidoresId)
                return false;

            AmigoDomain amigoDomain = await AmigoRepository.GetById(amigoDomainId);
            AmigoDomain amigosSeguidores = await AmigoRepository.GetById(amigosSeguidoresId);

            if (amigoDomain == null || amigosSeguidores == null)
                return false;

            AmigoDosAmigos amigoDosAmigos = new AmigoDosAmigos { AmigoDomainId = amigoDomainId, AmigoDosAmigosId = amigosSeguidoresId };
            await AmigoRepository.AddSeguidor(amigoDosAmigos);
            return true;
        }

        public async Task<bool> ExcluirRemover(Guid id, Guid amigoDomainId, Guid amigosSeguidoresId)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetById(amigoDomainId);
            AmigoDomain amigosSeguidores = await AmigoRepository.GetById(amigosSeguidoresId);
            if (amigoDomain == null || amigosSeguidores == null)
                return false;

            AmigoDosAmigos amigoDosAmigos = new AmigoDosAmigos { AmigoDomainId = amigoDomainId, AmigoDosAmigosId = amigosSeguidoresId };
            AmigoRepository.RemoveSeguidor(amigoDosAmigos);
            return true;
        }

        public async Task<bool> RemoverOk([FromRoute] Guid id, [FromBody] Guid amigoDomainId, Guid amigosSeguidoresId)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetById(amigoDomainId);
            AmigoDomain amigosSeguidores = await AmigoRepository.GetById(amigosSeguidoresId);
            if (amigoDomain == null || amigosSeguidores == null)
                return false;

            AmigoDosAmigos amigoDosAmigos = new AmigoDosAmigos { AmigoDomainId = amigoDomainId, AmigoDosAmigosId = amigosSeguidoresId };
            AmigoRepository.RemoveSeguidor(amigoDosAmigos);
            return true;
        }



        public AmigoServices(CasaDaHortaContext casaDaHortaContext, IConfiguration configuration)
        {
            this.Context = casaDaHortaContext;
            this.Configuration = configuration;
        }
        public string Login(string username, string senha)
        {
            var usuario = Context.Login.Where(x => x.Login == username && x.Password == senha).FirstOrDefault();

            if (usuario == null)
                return null;

            return CreateToken(usuario);
        }

        private string CreateToken(Conta amigoDomain)
        {
            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, amigoDomain.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, amigoDomain.Login));

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


        public void Delete(Guid id)
        {
            AmigoRepository.Delete(id);
        }
        


        public void Save(AmigoDomain amigoDomain, Guid id)
        {
            if (this.GetByEmail(amigoDomain.Email) != null)
            {
                throw new Exception("Já existe um aluno com este email, por favor cadastre outro email");
            }

            var anoAtual = DateTime.Now.Date.Year;

            if ((anoAtual - Convert.ToDateTime(amigoDomain.Datanascimento).Date.Year) < 21)
            {
                throw new Exception("Para cadastrar um novo aluno ele deve no minimo 21 anos");
            }

            this.AmigoRepository.Save(amigoDomain);
        }
        public void SaveAmigoDomain(AmigoDomain amigoDomain)
        {
            if (this.GetByEmail(amigoDomain.Email) != null)
            {
                throw new Exception("Já existe um aluno com este email, por favor cadastre outro email");
            }

            var anoAtual = DateTime.Now.Date.Year;

            if ((anoAtual - Convert.ToDateTime(amigoDomain.Datanascimento).Date.Year) < 21)
            {
                throw new Exception("Para cadastrar um novo aluno ele deve no minimo 21 anos");
            }

            this.AmigoRepository.Save(amigoDomain);
        }


        //public string Login(string login, string senha)
        //{
        //    var usuario = AmigoRepository.AmigoDomain.Where(x => x.Login == login && x.Password == senha).FirstOrDefault();

        //    if (usuario == null)
        //        return null;

        //    return CreateToken(usuario);
        //}
    }
}
