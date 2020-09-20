using CasaDaHora.Domain.Amigo;
using CasaDaHorta.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasaDaHorta.Repository
{
    public class RoleRepository : IRoleStore<RoleDomain>
    {
        private bool disposedValue;
        private CasaDaHortaContext Context { get; set; }
        public RoleRepository(CasaDaHortaContext casaDaHortaContext)
        {
            this.Context = casaDaHortaContext;
        }


        public async Task<IdentityResult> CreateAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            this.Context.Profiles.Add(role);
            await this.Context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            this.Context.Profiles.Remove(role);
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

        public async Task<RoleDomain> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await this.Context.Profiles.FirstOrDefaultAsync(x => x.Id == new Guid(roleId));
        }

        public async Task<RoleDomain> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await this.Context.Profiles.FirstOrDefaultAsync(x => x.Nome == normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Nome);
        }

        public Task<string> GetRoleIdAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Nome);
        }

        public Task SetNormalizedRoleNameAsync(RoleDomain role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Nome = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(RoleDomain role, string roleName, CancellationToken cancellationToken)
        {
            role.Nome = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(RoleDomain role, CancellationToken cancellationToken)
        {
            var roleToUpdate = await this.Context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == role.Id);

            roleToUpdate = role;
            this.Context.Entry(roleToUpdate).State = EntityState.Modified;

            this.Context.Profiles.Add(roleToUpdate);
            await this.Context.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}
