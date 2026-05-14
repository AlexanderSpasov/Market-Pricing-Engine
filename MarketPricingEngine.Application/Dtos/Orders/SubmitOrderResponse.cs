namespace MarketPricingEngine.Application.Dtos.Orders
{
    public class SubmitOrderResponse
    {
        public string ExistingOrderId { get; set; }

        public string Symbol { get; set; }

        public string Status { get; set; }

        public string? RejectedReason { get; set; }

        public decimal NotionalAmount { get; set; }
    }
}