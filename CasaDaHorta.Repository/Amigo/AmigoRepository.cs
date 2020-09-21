using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using CasaDaHorta.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace CasaDaHorta.Repository.Amigo
{
    public class AmigoRepository : IUserStore<AmigoDomain>, IAmigoRepository
    {
        private bool disposedValue;
        private CasaDaHortaContext Context { get; set; }
        public AmigoRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.Context = casaDaHortaContext;
        }

        public async Task<IdentityResult> CreateAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            this.Context.Amigos.Add(user);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            this.Context.Amigos.Remove(user);
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
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        public Task<AmigoDomain> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return this.Context.Amigos.FirstOrDefaultAsync(x => x.Id == new Guid(userId));
        }

        public  Task<AmigoDomain> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return this.Context.Amigos.FirstOrDefaultAsync(x => x.Nome == normalizedUserName);
        }

        public Task<AmigoDomain> GetAmigoByEmailPassword(string email, string password)
        {
            return Task.FromResult(this.Context.Amigos
                                               .FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public Task<AmigoDomain> GetAmigoByNomePassword(string Nome, string password)
        {
            return Task.FromResult(this.Context.Amigos
                                               .FirstOrDefault(x => x.Nome == Nome && x.Password == password));
        }

        public Task<string> GetNormalizedUserNameAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nome);
        }

        public Task<string> GetUserIdAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nome.ToString());
        }

        public Task SetNormalizedUserNameAsync(AmigoDomain user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Nome = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(AmigoDomain amigoDomain, string userName, CancellationToken cancellationToken)
        {
            amigoDomain.Nome = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(AmigoDomain user, CancellationToken cancellationToken)
        {
            var amigoToUpdate = await this.Context.Amigos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id);

            amigoToUpdate = user;
            this.Context.Entry(amigoToUpdate).State = EntityState.Modified;

            this.Context.Amigos.Add(amigoToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task CreateAmigo(AmigoDomain amigoDomain)
        {
            try
            {
                await Context.Amigos.AddAsync(amigoDomain);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateAmigo(AmigoDomain amigoDomain)
        {
            try
            {
                Context.Amigos.Update(amigoDomain);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<AmigoDomain> GetByEmail(string email)
        {
            return await Context.Amigos.FirstOrDefaultAsync(x => x.Email == email);
        }
        public AmigoDomain GetAmigoDomainByEmail(string email)
        {
            return Context.Amigos.FirstOrDefault(x => x.Email == email);
        }

        public async Task<AmigoDomain> GetById(Guid id)
        {
            return await Context.Amigos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AmigoDomain>> GetAll()
        {
            return await Context.Amigos.ToListAsync();
        }

        public async Task<List<AmigoSeguidorResponse>> GetAmigoDosAmigos(AmigoDomain amigoDomain)
        {
            amigoDomain.AmigosQueSeguem = await Context.MeuAmigoTemAmigos.Where(x => x.AmigoDomainId == amigoDomain.Id).ToListAsync();

            List<AmigoSeguidorResponse> amigosQueSeguem = new List<AmigoSeguidorResponse>();

            foreach (AmigoDosAmigos t in amigoDomain.AmigosQueSeguem)
            {
                AmigoDomain amigoEncontrado = await Context.Amigos.FirstOrDefaultAsync(amigoDomain => amigoDomain.Id == t.AmigoDosAmigosId);
                amigosQueSeguem.Add(new AmigoSeguidorResponse
                {
                    Id = amigoEncontrado.Id,
                    Nome = amigoEncontrado.Nome,
                    Sobrenome = amigoEncontrado.Sobrenome,
                    Email = amigoEncontrado.Email,
                    Datanascimento = amigoEncontrado.Datanascimento,
                    UrlFoto = amigoEncontrado.UrlFoto
                });
            }

            return amigosQueSeguem;
        }

        public async Task AddSeguidor(AmigoDosAmigos amigoDosAmigos)
        {
            try
            {
                await Context.MeuAmigoTemAmigos.AddAsync(amigoDosAmigos);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void RemoveSeguidor(AmigoDosAmigos amigoDosAmigos)
        {
            try
            {
                Context.MeuAmigoTemAmigos.Remove(amigoDosAmigos);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void Save(AmigoDomain amigoDomain)
        {
            this.Context.Amigos.Add(amigoDomain);
            this.Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var aluno = Context.Amigos.FirstOrDefault(x => x.Id == id);
            this.Context.Amigos.Remove(aluno);
            this.Context.SaveChanges();
        }
    }
}
