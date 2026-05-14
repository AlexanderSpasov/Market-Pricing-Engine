using MarketPricingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPricingEngine.Infrastructure.Persistence.Mappings
{
    public class LatestPriceMapping : IEntityTypeConfiguration<LatestPriceEntity>
    {
        public void Configure(EntityTypeBuilder<LatestPriceEntity> builder)
        {
            builder.ToTable("latest_prices");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");
            
            builder
                .Property(x => x.Symbol)
                .HasColumnName("symbol")
                .IsRequired();
            
            builder
                .HasIndex(x => x.Symbol)
                .IsUnique();

            builder
                .Property(x => x.BidPrice)
                .HasColumnName("bid_price")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.AskPrice)
                .HasColumnName("ask_price")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.CurrentMarketPrice)
                .HasColumnName("current_market_price")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.Spread)
                .HasColumnName("spread")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.SpreadPercent)
                .HasColumnName("spread_percent")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.TimeStamp)
                .HasColumnName("timestamp");
        }
    }
}