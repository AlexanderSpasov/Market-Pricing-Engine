using MarketPricingEngine.Domain.Enums;

namespace MarketPricingEngine.Domain.Entities
{
    public class OrderEntity
    {
        public long Id { get; set; }

        public string ExternalOrderId { get; set; }

        public string Symbol { get; set; }

        public OrderSide Side { get; set; }

        public OrderType Type { get; set; }

        public decimal RequestedPrice {get; set;}

        public long Quantity { get; set; }

        public decimal NotionalAmount { get; set; }

        public OrderSource Source { get; set; }

        public OrderStatus Status { get; set; }

        public string? RejectionReason { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}