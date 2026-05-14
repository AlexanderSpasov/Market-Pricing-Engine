using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Domain.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarketPricingEngine.Infrastructure.BackgroundServices
{
    public class MarketPriceBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public MarketPriceBackgroundService (IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var tasks = MarketSymbols.AllSymbols.Select(x => this.GenerateForSymbol(x));

                await Task.WhenAll(tasks);

                await Task.Delay(2000, stoppingToken);
            }
        }

        private async Task GenerateForSymbol(string symbol)
        {
            using var scope = this.serviceScopeFactory.CreateScope();

            var priceFeedService = scope.ServiceProvider.GetRequiredService<IPriceFeedService>();

            await priceFeedService.GenerateAndProcessTickAsync(symbol);
        }
    }
}