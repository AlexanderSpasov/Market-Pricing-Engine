using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Services
{
    public interface IPriceFeedService
    {
        Task<PriceTickEntity> GenerateAndProcessTickAsync(string symbol);
    }
}