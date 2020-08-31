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
    public class AccountRepository : IUserStore<Accounty>, IAccountRepository
    {
        private bool disposedValue;
        private CasaDaHortaContext Context { get; set; }
        public AccountRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.Context = casaDaHortaContext;
        }
        public async Task<IdentityResult> CreateAsync(Accounty user, CancellationToken cancellationToken)
        {
            this.Context.Accounts.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Accounty user, CancellationToken cancellationToken)
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
        public Task<Accounty> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.Id == new Guid(userId));
        }

        public Task<Accounty> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return this.Context.Accounts.FirstOrDefaultAsync(x => x.Nome == normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Accounty user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(Accounty user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Accounty user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.ToString());
        }

        public Task SetNormalizedUserNameAsync(Accounty user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Accounty user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Accounty user, CancellationToken cancellationToken)
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
        public Task<Accounty> GetAccountByEmailPassword(string email, string password)
        {
            return Task.FromResult(this.Context.Accounts
                                               .Include(x => x.Role)
                                               .FirstOrDefault(x => x.Email == email && x.Password == password));
        }
        public Task<Accounty> GetAccountByUserNamePassword(string userName, string password)
        {
            return Task.FromResult(this.Context.Accounts
                                               .Include(x => x.Role)
                                               .FirstOrDefault(x => x.UserName == userName && x.Password == password));
        }

    }
}
