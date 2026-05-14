namespace MarketPricingEngine.Application.Dtos.TradingRules
{
    public class TradingDecision
    {
        public bool IsAccepted { get; set; }

        public List<string> RejectedReasons { get; set; } = new();
    }
}