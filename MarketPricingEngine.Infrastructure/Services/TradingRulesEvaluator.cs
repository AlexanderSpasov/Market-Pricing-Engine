using System.Data;
using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Application.Dtos.TradingRules;
using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Infrastructure.Services
{
    public class TradingRulesEvaluator : ITradingRulesEvaluator
    {
        private readonly IOrderRepository orderRepository;
        private readonly ITradingRulesRepository tradingRulesRepository;
        private readonly ILatestPriceRepository latestPriceRepository;

        public TradingRulesEvaluator(
            IOrderRepository orderRepository,
            ITradingRulesRepository tradingRulesRepository,
            ILatestPriceRepository latestPriceRepository)
        {
            this.orderRepository = orderRepository;
            this.tradingRulesRepository = tradingRulesRepository;
            this.latestPriceRepository = latestPriceRepository;
        }

        public async Task<TradingDecision> EvaluateAsync(OrderEntity order)
        {
            var decision = new TradingDecision();

            var rules = await this.tradingRulesRepository.GetAsync();
            var latestPrice = await this.latestPriceRepository.GetLatestPriceBySymbol(order.Symbol);
            

            if (rules == null)
            {
                decision.RejectedReasons.Add("Trading rules are not configured");
            }

            if (latestPrice == null)
            {
                decision.RejectedReasons.Add($"No latest price found for symbol {order.Symbol}");
            }

            if (rules != null)
            {
                if (order.NotionalAmount > rules.MaxNotionalAmount)
                {
                    decision.RejectedReasons.Add("Order notional amount exceeds allowed maximum");
                }

                if (order.Quantity > rules.MaxQuantity)
                {
                    decision.RejectedReasons.Add("Order quantity exceeds allowed maximum");
                }

                if (rules.RejectDuplicateOrderIds)
                {
                    var duplicateExists = await this.orderRepository.CheckIfExistsByExternalIdAsync(order.ExternalOrderId);
                    if (duplicateExists)
                    {
                        decision.RejectedReasons.Add("Duplicate order external ID");
                    }
                }

                if (rules.EnableSymbolWhitelist)
                {
                    var allowedSymbols = rules.AllowedSymbolsCsv
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    if (!allowedSymbols.Contains(order.Symbol, StringComparer.OrdinalIgnoreCase))
                    {
                        decision.RejectedReasons.Add("Symbol is not allowed");
                    }
                }
            }

            if (rules != null && latestPrice != null)
            {
                var threshold = rules.MaxPriceDeviationPercent > 0
                ? rules.MaxPriceDeviationPercent
                : 0.8m;

                var deviationPercent =
                    Math.Abs(order.RequestedPrice - latestPrice.CurrentMarketPrice)
                    / latestPrice.CurrentMarketPrice
                    * 100;

                if (deviationPercent > threshold)
                {
                    decision.RejectedReasons.Add("Price deviation exceeds allowed threshold");
                }

            }

            decision.IsAccepted = !decision.RejectedReasons.Any();

            return decision;
        }
    }
}