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
        void UpdateAmigo(AmigoDomain amigoDomain);
        Task<AmigoDomain> GetByEmail(string email);
        Task<AmigoDomain> GetById(Guid id);
        Task<List<AmigoDomain>> GetAll();
        Task<List<AmigoSeguidorResponse>> GetAmigoDosAmigos(AmigoDomain amigoDomain);
        Task AddFollower(AmigoDosAmigos amigoDosAmigos);
        void RemoveFollower(AmigoDosAmigos amigoDosAmigos);
    }
}
