using CasaDaHora.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CasaDaHorta.Repository.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);
            builder.Property(x => x.DataNascimento).IsRequired();

            builder.HasOne(x => x.Role).WithMany(x => x.Accounts);

        }
    }
}
