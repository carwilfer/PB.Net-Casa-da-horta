using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using CasaDaHora.Domain.Comment;
using CasaDaHora.Domain.Post;
using CasaDaHorta.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Repository.Context
{
    public class CasaDaHortaContext : DbContext
    {
        public DbSet<AmigoDomain> Amigos { get; set; }
        public DbSet<AmigoDosAmigos> MeuAmigoTemAmigos { get; set; }
        public DbSet<AmigoSeguidorResponse> AmigosSeguidorResponse { get; set; }
        public DbSet<Accounty> Accounts { get; set; }
        public DbSet<Conta> Login { get; set; }
        public DbSet<RoleDomain> Profiles { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comment { get; set; }


        public static readonly ILoggerFactory _loggerFactory
                = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public CasaDaHortaContext(DbContextOptions<CasaDaHortaContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new AmigoMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new ContaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
