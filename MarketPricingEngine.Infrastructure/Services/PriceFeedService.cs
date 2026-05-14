using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Infrastructure.Services
{
    public class PriceFeedService : IPriceFeedService
    {
        private readonly IMarketPriceGenerator marketPriceGenerator;
        private readonly IPriceTickRepository priceTickRepository;
        private readonly ILatestPriceRepository latestPriceRepository;
        private readonly IAutoTradingService autoTradingService;

        public PriceFeedService(
            IMarketPriceGenerator marketPriceGenerator,
            IPriceTickRepository priceTickRepository,
            ILatestPriceRepository latestPriceRepository,
            IAutoTradingService autoTradingService)
        {
            this.marketPriceGenerator = marketPriceGenerator;
            this.priceTickRepository = priceTickRepository;
            this.latestPriceRepository = latestPriceRepository;
            this.autoTradingService = autoTradingService;
        }

        public async Task<PriceTickEntity> GenerateAndProcessTickAsync(string symbol)
        {
            var previousPrice = await this.latestPriceRepository.GetLatestPriceBySymbol(symbol);

            var tick = await this.marketPriceGenerator.GeneratePrice(symbol);

            await this.priceTickRepository.AddAsync(tick);

            await this.latestPriceRepository.UpdateAsync(tick);

            if (previousPrice != null)
            {
                await this.autoTradingService.EvaluateAsync(tick, previousPrice);
            }

            return tick;
        }
    }
}