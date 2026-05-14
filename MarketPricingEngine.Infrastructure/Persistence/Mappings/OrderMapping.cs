using MarketPricingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPricingEngine.Infrastructure.Persistence.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");
            
            builder
                .Property(x => x.ExternalOrderId)
                .HasColumnName("external_order_id")
                .IsRequired();

            builder
                .HasIndex(x => x.ExternalOrderId);

            builder
                .Property(x => x.Symbol)
                .HasColumnName("symbol")
                .IsRequired();

            builder
                .HasIndex(x => x.Symbol);

            builder
                .Property(x => x.Side)
                .HasColumnName("side")
                .HasConversion<string>();

            builder
                .Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<string>();

            builder
                .Property(x => x.RequestedPrice)
                .HasColumnName("requested_price")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.Quantity)
                .HasColumnName("quantity");

            builder
                .Property(x => x.NotionalAmount)
                .HasColumnName("notional_amount")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.Source)
                .HasColumnName("source")
                .HasConversion<string>();

            builder
                .Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<string>();

            builder
                .Property(x => x.RejectionReason)
                .HasColumnName("rejection_reason");

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}