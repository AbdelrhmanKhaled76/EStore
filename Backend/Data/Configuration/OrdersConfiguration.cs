using EStore.Models;
using EStore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Status)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PaymentMethod)
                .HasConversion(x => x.ToString(),
                   x => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), x)
                   );

            builder.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("NOW()");

            builder.HasOne(x => x.Transaction)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Client)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }

}
