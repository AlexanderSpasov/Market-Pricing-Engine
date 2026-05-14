using MarketPricingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPricingEngine.Infrastructure.Persistence.Mappings
{
    public class PriceTickMapping : IEntityTypeConfiguration<PriceTickEntity>
    {
        public void Configure(EntityTypeBuilder<PriceTickEntity> builder)
        {
            builder.ToTable("price_ticks");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Symbol)
                .HasColumnName("symbol")
                .IsRequired();

            builder
                .Property(x => x.BidPrice)
                .HasColumnName("bid_price")
                .HasColumnType("decimal(18,6)");

            builder.Property(x => x.AskPrice)
                .HasColumnName("ask_price")
                .HasColumnType("decimal(18,6)");

            builder.Property(x => x.CurrentMarketPrice)
                .HasColumnName("mid_price");

            builder.Property(x => x.Spread)
                .HasColumnName("spread");

            builder.Property(x => x.SpreadPercent)
                .HasColumnName("spread_percent");

            builder.Property(x => x.TimeStamp)
                .HasColumnName("timestamp");
        }
    }
}