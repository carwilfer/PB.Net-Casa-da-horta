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
    }
}
