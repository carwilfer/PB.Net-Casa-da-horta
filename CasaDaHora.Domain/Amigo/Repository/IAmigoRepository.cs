using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Amigo.Repository
{
    public interface IAmigoRepository
    {
        Task<AmigoDomain> GetAmigoByEmailPassword(string email, string password);
        Task<AmigoDomain> GetAmigoByNomePassword(string Nome, string password);
        Task CreateAmigo(AmigoDomain amigoDomain);
        Task<IdentityResult> DeleteAsync(Guid id, string nome);
        void UpdateAmigo(AmigoDomain amigoDomain);
        Task<AmigoDomain> GetByEmail(string email);
        Task<AmigoDomain> GetById(Guid id);
        Task<List<AmigoDomain>> GetAll();
        Task<List<AmigoSeguidorResponse>> GetAmigoDosAmigos(AmigoDomain amigoDomain);
        Task AddSeguidor(AmigoDosAmigos amigoDosAmigos);
        void RemoveSeguidor(AmigoDosAmigos amigoDosAmigos);
    }
}
