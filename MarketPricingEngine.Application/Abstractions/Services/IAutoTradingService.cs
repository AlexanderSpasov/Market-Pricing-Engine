using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Services
{
    public interface IAutoTradingService
    {
        Task EvaluateAsync(PriceTickEntity latestTick, LatestPriceEntity? previousPrice);
    }
}