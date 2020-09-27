using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Repository.Context
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<CasaDaHortaContext>
    {
        public CasaDaHortaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CasaDaHortaContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CasaDaHorta;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new CasaDaHortaContext(optionsBuilder.Options);
        }
    }
}
