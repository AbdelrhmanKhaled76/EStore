using EStore.Models;
using EStore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class ProductsColorsConfiguration : IEntityTypeConfiguration<ProductColors>
    {
        public void Configure(EntityTypeBuilder<ProductColors> builder)
        {
            builder.ToTable("ProductColors");

            builder.HasKey(x => new { x.ProductId, x.Color });

            builder.Property(x => x.Color)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(30)
               .HasConversion(
                    x => x.ToString(),
                    x => Enum.Parse<ProductColor>(x)      
               );

            builder.HasOne(x => x.Product)
            .WithMany(x => x.Colors);

        }
    }
}
