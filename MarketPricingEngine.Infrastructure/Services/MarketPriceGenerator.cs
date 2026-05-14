using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Infrastructure.Configurations;

namespace MarketPricingEngine.Infrastructure.Services
{
    public class MarketPriceGenerator : IMarketPriceGenerator
    {
        private readonly MarketSimulationConfiguration marketSimulationConfiguration;

        public MarketPriceGenerator(MarketSimulationConfiguration marketSimulationConfiguration)
        {
            this.marketSimulationConfiguration = marketSimulationConfiguration;
        }
        public Task<PriceTickEntity> GeneratePrice(string symbol)
        {
            var basePrice = this.marketSimulationConfiguration.BasePrices[symbol];
            var movementPercent = Random.Shared.Next(-100, 101) / 10000m;
            var midPrice = basePrice + (basePrice * movementPercent);

            var spreadPercent = Random.Shared.Next(1, 20) / 10000m;
            var spread = midPrice * spreadPercent;

            var bidPrice = midPrice - (spread / 2);
            var askPrice = midPrice + (spread / 2);

            var tick = new PriceTickEntity(symbol, bidPrice, askPrice);

            return Task.FromResult(tick);
        }
    }
}