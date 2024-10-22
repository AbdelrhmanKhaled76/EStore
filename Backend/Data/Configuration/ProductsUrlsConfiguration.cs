using EStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class ProductsUrlsConfiguration : IEntityTypeConfiguration<ProductUrls>
    {
        public void Configure(EntityTypeBuilder<ProductUrls> builder)
        {
            builder.ToTable("ProductsUrls");

            builder.HasKey(x => new { x.ProductId, x.Url });


            builder.HasOne(x => x.Product)
                .WithMany(x => x.Urls);
        }
    }

}
