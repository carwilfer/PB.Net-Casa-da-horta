using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Account.Repository;
using CasaDaHorta.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Rest;

namespace CasaDaHorta.Repository.AccountRepository
{
    public class AccountRepository : IUserStore<Account>, IAccountRepository
    {
        private bool disposedValue;
        private CasaDaHortaContext Context { get; set; }
        public AccountRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.Context = casaDaHortaContext;
        }
        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            this.Context.Accounts.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            this.Context.Accounts.Remove(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
        public Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.Id == new Guid(userId));
        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.Nome == normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToString());
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            //não guarda em memória
            var accountToUpdate = await this.Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id);
            //faz atualização
            accountToUpdate = user;
            this.Context.Entry(accountToUpdate).State = EntityState.Modified;
            //adiciona pra fazer memória novamente
            this.Context.Accounts.Add(accountToUpdate);
            //Manda as atualizações para BD
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }
        public Task<Account> GetAccountByEmailPassword(string email, string password)
        {
            return Task.FromResult(this.Context.Accounts
                                               .Include(x => x.Role)
                                               .FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public async Task<IdentityResult> CreateUser(string nome, DateTime dataNascimento, string email, string password)
        {
            CancellationToken cancellationToken;
            var user = new Account
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                Email = email,
                Password = password,
                Role = new Role
                {
                    Id = new Guid(),
                    Nome = "USUARIO",
                }
            };
            await CreateAsync(user, cancellationToken);
            return IdentityResult.Success;
        }

    }
}
