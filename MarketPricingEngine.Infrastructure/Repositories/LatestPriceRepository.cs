using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketPricingEngine.Infrastructure.Repositories
{
    public class LatestPriceRepository : ILatestPriceRepository
    {
        private readonly MarketPricingDbContext context;

        public LatestPriceRepository(MarketPricingDbContext context)
        {
            this.context = context;
        }
        public async Task<LatestPriceEntity?> GetLatestPriceBySymbol(string symbol)
            => await this.context.LatestPrices.FirstOrDefaultAsync(x => x.Symbol == symbol);

        public async Task UpdateAsync(PriceTickEntity entity)
        {
            var existingLatestPrice = await this.context.LatestPrices
                .FirstOrDefaultAsync(x => x.Symbol == entity.Symbol);
            
            if (existingLatestPrice == null)
            {
                var latest = new LatestPriceEntity
                {
                    Symbol = entity.Symbol,
                    BidPrice = entity.BidPrice,
                    AskPrice = entity.AskPrice,
                    CurrentMarketPrice = entity.CurrentMarketPrice,
                    Spread = entity.Spread,
                    SpreadPercent = entity.SpreadPercent,
                    TimeStamp = entity.TimeStamp
                };

                await this.context.LatestPrices.AddAsync(latest);
            }
            else
            {
                existingLatestPrice.BidPrice = entity.BidPrice;
                existingLatestPrice.AskPrice = entity.AskPrice;
                existingLatestPrice.CurrentMarketPrice = entity.CurrentMarketPrice;
                existingLatestPrice.Spread = entity.Spread;
                existingLatestPrice.SpreadPercent = entity.SpreadPercent;
                existingLatestPrice.TimeStamp = entity.TimeStamp;
            }

            await this.context.SaveChangesAsync();
        }
    }
}