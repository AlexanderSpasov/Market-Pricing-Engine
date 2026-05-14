namespace MarketPricingEngine.Domain.Entities
{
    public class LatestPriceEntity
    {
        public long Id { get; set; }

        public string Symbol { get; set; }

        public decimal BidPrice { get; set; }

        public decimal AskPrice { get; set; }

        public decimal CurrentMarketPrice { get; set; }

        public decimal Spread { get; set; }

        public decimal SpreadPercent { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}