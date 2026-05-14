using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Repositories
{
    public interface ILatestPriceRepository
    {
        Task<LatestPriceEntity?> GetLatestPriceBySymbol(string symbol);

        Task UpdateAsync(PriceTickEntity entity);
    }
}