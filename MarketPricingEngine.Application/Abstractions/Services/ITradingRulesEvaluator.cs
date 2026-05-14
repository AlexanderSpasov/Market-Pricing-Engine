using MarketPricingEngine.Application.Dtos.TradingRules;
using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Services
{
    public interface ITradingRulesEvaluator
    {
        Task<TradingDecision> EvaluateAsync(OrderEntity entity);
    }
}