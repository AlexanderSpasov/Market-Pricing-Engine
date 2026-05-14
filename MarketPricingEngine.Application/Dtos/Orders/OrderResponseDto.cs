using MarketPricingEngine.Domain.Enums;

namespace MarketPricingEngine.Application.Dtos.Orders
{
    public class OrderResponseDto
    {
        public string ExternalOrderId { get; set; }

        public string Symbol { get; set; }

        public OrderSide Side { get; set; }

        public Decimal RequestedPrice { get; set; }

        public long Quantity { get; set; }
    }
}