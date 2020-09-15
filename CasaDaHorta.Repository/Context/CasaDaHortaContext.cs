using CasaDaHora.Domain.Account;
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
        public DbSet<Accounty> Accounts { get; set; }
        public DbSet<Role> Profiles { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment{ get; set; }

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
            modelBuilder.ApplyConfiguration(new RoleMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
