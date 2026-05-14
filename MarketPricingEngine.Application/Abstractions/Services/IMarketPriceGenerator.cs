using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Services
{
    public interface IMarketPriceGenerator
    {
        Task<PriceTickEntity> GeneratePrice(string symbol);
    }
}