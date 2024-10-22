using EStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FirstName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(x => x.Street)
                .IsUnicode();

        }
    }

}
