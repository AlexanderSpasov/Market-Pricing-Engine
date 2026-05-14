using MarketPricingEngine.Application.Dtos.Orders;

namespace MarketPricingEngine.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<SubmitOrderResponse> SubmitOrder(SubmitOrderRequest request);
    }
}