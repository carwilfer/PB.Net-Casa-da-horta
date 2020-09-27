using CasaDaHora.Domain.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CasaDaHorta.Repository.Mapping
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Content).IsRequired().HasMaxLength(250);
            builder.Property(x => x.ImagePost).HasMaxLength(250);

            builder.HasMany(x => x.Comments).WithOne(c => c.Post);
            builder.HasOne(x => x.Account).WithMany(x => x.Posts);

        }
    }
}
