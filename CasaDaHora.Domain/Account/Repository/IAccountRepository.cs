using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Account.Repository
{
    public interface IAccountRepository
    {
        Task<Accounty> GetAccountByEmailPassword(string email, string password);
        Task<Accounty> GetAccountByUserNamePassword(string userName, string password);
    }
}
