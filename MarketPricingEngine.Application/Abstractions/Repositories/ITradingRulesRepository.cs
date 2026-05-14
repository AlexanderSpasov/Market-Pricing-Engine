using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Repositories
{
    public interface ITradingRulesRepository
    {
        Task<TradingRulesEntity?> GetAsync();

        Task UpsertAsync(TradingRulesEntity entity);
    }
}