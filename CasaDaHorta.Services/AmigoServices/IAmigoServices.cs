using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.Services.AmigoServices
{
    public interface IAmigoServices
    {
        IAmigoRepository AmigoRepository { get; set; }

        Task<AmigoDomainResponse> CreateAmigoDomain(Guid Id, string Nome, string Sobrenome, string email, string password, string UrlFoto);

        Task<AmigoDomain> GetAmigoDomainByEmail(string email);

        Task<AmigoDomainResponse> GetByEmail(string email);

        Task<AmigoDomain> GetAmigoDomainById(Guid id);

        Task<AmigoDomainResponse> GetById(Guid id);
        Task<List<AmigoDomainResponse>> GetAll();

        Task<bool> UpdateAmigoDomain(Guid id, string Nome, string Sobrenome, string Email, string Password, string UrlFoto);

        Task<List<AmigoSeguidorResponse>> GetSeguidores(Guid userId);

        Task<bool> AddSeguidores(Guid amigoDomainId, Guid amigosSeguidoresId);

        Task<bool> ExcluirRemover(Guid id, Guid amigoDomainId, Guid amigosSeguidoresId);
    }
}
