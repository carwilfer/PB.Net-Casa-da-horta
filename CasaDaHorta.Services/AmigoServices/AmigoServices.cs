using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using CasaDaHorta.Services.AmigoServices.AjudaPadrão;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.AmigoServices
{
    public class AmigoServices : IAmigoServices
    {
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
                    Password = t.Password,
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

        public async Task<bool> ExcluirRemover(Guid amigoDomainId, Guid amigosSeguidoresId)
        {
            AmigoDomain amigoDomain = await AmigoRepository.GetById(amigoDomainId);
            AmigoDomain amigosSeguidores = await AmigoRepository.GetById(amigosSeguidoresId);
            if (amigoDomain == null || amigosSeguidores == null)
                return false;

            AmigoDosAmigos amigoDosAmigos = new AmigoDosAmigos { AmigoDomainId = amigoDomainId, AmigoDosAmigosId = amigosSeguidoresId };
            AmigoRepository.RemoveSeguidor(amigoDosAmigos);
            return true;
        }
    }
}
