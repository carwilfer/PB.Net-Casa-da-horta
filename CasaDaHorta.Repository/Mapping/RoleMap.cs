using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Amigo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Repository.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<RoleDomain>
    {
        public void Configure(EntityTypeBuilder<RoleDomain> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);

        }
    }
}

