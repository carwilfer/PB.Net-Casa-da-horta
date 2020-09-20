using CasaDaHora.Domain.Amigo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Repository.Mapping
{
    public class AmigoMap : IEntityTypeConfiguration<AmigoDomain>
    {
        public void Configure(EntityTypeBuilder<AmigoDomain> builder)
        {
            builder.ToTable("Amigos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasMaxLength(50);
            builder.Property(x => x.Sobrenome).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.Datanascimento);
            builder.Property(x => x.Password);
        }
    }
}
