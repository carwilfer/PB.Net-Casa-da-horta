using CasaDaHora.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Twilio.Rest;

namespace CasaDaHorta.Repository.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Accounty>
    {
        public void Configure(EntityTypeBuilder<Accounty> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.SobreNome).IsRequired().HasMaxLength(250);
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);
            //direção de navegação unica
            builder.HasOne(x => x.Role).WithMany(x => x.Accounts);

        }
    }
}
