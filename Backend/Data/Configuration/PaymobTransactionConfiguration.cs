using EStore.Models;
using EStore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Data.Configuration
{
    public class PaymobTransactionConfiguration : IEntityTypeConfiguration<PaymobTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymobTransaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();


        }
    }

}
