using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Infrastructure.BackgroundServices;
using MarketPricingEngine.Infrastructure.Configurations;
using MarketPricingEngine.Infrastructure.Repositories;
using MarketPricingEngine.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPricingEngine.Infrastructure;

public static class DependencyResolver
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPriceTickRepository, PriceTickRepository>();
        services.AddScoped<ILatestPriceRepository, LatestPriceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITradingRulesRepository, TradingRulesRepository>();

        services.AddScoped<IMarketPriceGenerator, MarketPriceGenerator>();
        services.AddScoped<IPriceFeedService, PriceFeedService>();
        services.AddScoped<IAutoTradingService, AutoTradingService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ITradingRulesEvaluator, TradingRulesEvaluator>();

        services.AddHostedService<MarketPriceBackgroundService>();

        services.AddSingleton<MarketSimulationConfiguration>();

        return services;
    }
}