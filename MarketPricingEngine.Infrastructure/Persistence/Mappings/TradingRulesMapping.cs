using MarketPricingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPricingEngine.Infrastructure.Persistence.Mappings
{
    public class TradingRulesMapping : IEntityTypeConfiguration<TradingRulesEntity>
    {
        public void Configure(EntityTypeBuilder<TradingRulesEntity> builder)
        {
            builder.ToTable("trading_rules");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.MaxNotionalAmount)
                .HasColumnName("max_notional_amount")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.MaxQuantity)
                .HasColumnName("max_quantity");

            builder
                .Property(x => x.MaxPriceDeviationPercent)
                .HasColumnName("max_price_deviation_percent")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.RejectDuplicateOrderIds)
                .HasColumnName("reject_duplicate_order_ids");

            builder
                .Property(x => x.EnableSymbolWhitelist)
                .HasColumnName("enable_symbol_whitelist");

            builder
                .Property(x => x.AllowedSymbolsCsv)
                .HasColumnName("allowed_symbols_csv");

            builder
                .Property(x => x.AutoTradingSpreadThresholdPercent)
                .HasColumnName("auto_trading_spread_threshold_percent")
                .HasColumnType("decimal(18,6)");

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");
        }
    }
}