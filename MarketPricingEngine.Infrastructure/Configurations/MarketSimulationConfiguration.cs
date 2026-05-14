namespace MarketPricingEngine.Infrastructure.Configurations
{
    public class MarketSimulationConfiguration
    {
        public Dictionary<string, decimal> BasePrices { get; set; } = new Dictionary<string, decimal>
        {
            ["EURUSD"] = 1.08m,
            ["GBPUSD"] = 1.26m,
            ["USDJPY"] = 155m,
            ["BTCUSD"] = 65000m,
            ["ETHUSD"] = 3200m,
            ["AAPL"] = 190m,
            ["TSLA"] = 180m,
            ["MSFT"] = 420m,
            ["GOOGL"] = 170m,
            ["AMZN"] = 185m
        };
    }
}