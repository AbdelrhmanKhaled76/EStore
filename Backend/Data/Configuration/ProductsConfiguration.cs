using EStore.Models;
using EStore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsUnicode(true)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(x=> x.Type)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50)
                .HasConversion(
                    x => x.ToString(),
                    x => Enum.Parse<ProductType>(x)
               ); ;


                
        }
    }
}
