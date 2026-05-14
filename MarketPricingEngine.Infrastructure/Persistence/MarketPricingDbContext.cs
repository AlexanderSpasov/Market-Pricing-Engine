using MarketPricingEngine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketPricingEngine.Infrastructure.Persistence
{
    public class MarketPricingDbContext : DbContext
    {
        public MarketPricingDbContext(DbContextOptions<MarketPricingDbContext> options)
            : base(options)
        {
        }

        public DbSet<PriceTickEntity> PriceTicks { get; set; }

        public DbSet<LatestPriceEntity> LatestPrices { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<TradingRulesEntity> TradingRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketPricingDbContext).Assembly);
        }
    }
}