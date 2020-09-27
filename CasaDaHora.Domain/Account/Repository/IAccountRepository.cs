using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Account.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByEmailPassword(string email, string password);
        Task<IdentityResult> CreateUser(string nome, DateTime dataNascimento, string email, string password);
        Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken);

    }
}
