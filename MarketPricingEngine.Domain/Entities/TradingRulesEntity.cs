namespace MarketPricingEngine.Domain.Entities
{
    public class TradingRulesEntity
    {
        public long Id { get; set; }

        public decimal MaxNotionalAmount { get; set; }

        public long MaxQuantity { get; set; }

        public decimal MaxPriceDeviationPercent { get; set; } = 0.8m;

        public bool RejectDuplicateOrderIds { get; set; }

        public bool EnableSymbolWhitelist { get; set; }

        public string AllowedSymbolsCsv { get; set; }

        public decimal AutoTradingSpreadThresholdPercent { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}