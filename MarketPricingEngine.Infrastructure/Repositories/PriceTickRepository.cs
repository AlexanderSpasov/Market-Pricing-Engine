using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Infrastructure.Persistence;

namespace MarketPricingEngine.Infrastructure.Repositories
{
    public class PriceTickRepository : IPriceTickRepository
    {
        private readonly MarketPricingDbContext context;

        public PriceTickRepository(MarketPricingDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(PriceTickEntity entity)
        {
            await this.context.PriceTicks.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }
    }
}