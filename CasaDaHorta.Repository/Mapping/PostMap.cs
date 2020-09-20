using CasaDaHora.Domain.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Twilio.Rest;

namespace CasaDaHorta.Repository.Mapping
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Texto).IsRequired().HasMaxLength(250);
            builder.Property(x => x.UrlFoto).HasMaxLength(250);

            //builder.HasOne(x => x.Comments).WithMany(x => x.Comment);
            //builder.HasMany(x => Comments).WithOne(c => c.Post);
            builder.HasOne(x => x.Amigo);
        }
    }
}
