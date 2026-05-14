using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Application.Dtos.Orders;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Domain.Enums;

namespace MarketPricingEngine.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ITradingRulesEvaluator tradingRulesEvaluator;

        public OrderService(
            IOrderRepository orderRepository,
            ITradingRulesEvaluator tradingRulesEvaluator)
        {
            this.orderRepository = orderRepository;
            this.tradingRulesEvaluator = tradingRulesEvaluator;
        }

        public async Task<SubmitOrderResponse> SubmitOrder(SubmitOrderRequest request)
        {
            var order = new OrderEntity
            {
                ExternalOrderId = request.ExternalOrderId,
                Symbol = request.Symbol,
                Side = request.Side,
                Type = OrderType.Limit,
                RequestedPrice = request.RequestedPrice,
                NotionalAmount = request.RequestedPrice * request.Quantity,
                Quantity = request.Quantity,
                Source = OrderSource.API,
                CreatedAt = DateTime.UtcNow
            };

            var decision = await this.tradingRulesEvaluator.EvaluateAsync(order);

            order.Status = decision.IsAccepted
                ? OrderStatus.Accepted
                : OrderStatus.Rejected;

            order.RejectionReason = decision.IsAccepted
                ? null
                : String.Join(',', decision.RejectedReasons);


            await this.orderRepository.AddAsync(order);

            return new SubmitOrderResponse
            {
                ExistingOrderId = order.ExternalOrderId,
                Symbol = order.Symbol,
                Status = order.Status.ToString(),
                RejectedReason = order.RejectionReason,
                NotionalAmount = order.NotionalAmount
            };
        }
    }
}