using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketPricingEngine.Infrastructure.Repositories
{
    public class TradingRulesRepository : ITradingRulesRepository
    {
        private readonly MarketPricingDbContext context;

        public TradingRulesRepository(MarketPricingDbContext context)
        {
            this.context = context;
        }

        public async Task<TradingRulesEntity?> GetAsync()
            => await this.context.TradingRules.FirstOrDefaultAsync();

        public async Task UpsertAsync(TradingRulesEntity entity)
        {
            var existing = await this.context.TradingRules.FirstOrDefaultAsync();

            if(existing == null)
            {
                this.context.TradingRules.Add(entity);
            }
            else
            {
                existing.MaxNotionalAmount = entity.MaxNotionalAmount;
                existing.MaxQuantity = entity.MaxQuantity;
                existing.MaxPriceDeviationPercent = entity.MaxPriceDeviationPercent;
                existing.RejectDuplicateOrderIds = entity.RejectDuplicateOrderIds;
                existing.EnableSymbolWhitelist = entity.EnableSymbolWhitelist;
                existing.AllowedSymbolsCsv = entity.AllowedSymbolsCsv;
                existing.AutoTradingSpreadThresholdPercent = entity.AutoTradingSpreadThresholdPercent;
                existing.UpdatedAt = DateTime.UtcNow;
            }

            await this.context.SaveChangesAsync();
        }
    }
}