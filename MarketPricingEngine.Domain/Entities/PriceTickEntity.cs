namespace MarketPricingEngine.Domain.Entities
{
    public class PriceTickEntity
    {
        public PriceTickEntity()
        {
        }

        public PriceTickEntity(string symbol, decimal bidPrice, decimal askPrice)
        {
            if (bidPrice >= askPrice)
            {
                throw new InvalidOperationException("BidPrice must be lower than AskPrice");
            }

            this.Symbol = symbol;
            this.BidPrice = bidPrice;
            this.AskPrice = askPrice;
            this.CurrentMarketPrice = (bidPrice + askPrice) / 2;
            this.Spread = askPrice - bidPrice;
            this.SpreadPercent = this.Spread / this.CurrentMarketPrice * 100;
            this.TimeStamp = DateTime.UtcNow;
        }

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