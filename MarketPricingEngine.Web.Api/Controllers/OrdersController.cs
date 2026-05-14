using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Abstractions.Services;
using MarketPricingEngine.Application.Dtos.Orders;
using MarketPricingEngine.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MarketPricingEngine.Web.Api.Controllers
{
    /// <summary>
    /// Handles trade order operations (submit and history).
    /// </summary>
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderService orderService;

        public OrdersController(
            IOrderRepository orderRepository,
            IOrderService orderService)
        {
            this.orderRepository = orderRepository;
            this.orderService = orderService;
        }

        /// <summary>
        /// Submits a limit trade request and validates it against the configured trading rules.
        /// </summary>
        /// <param name="request">The trade request payload.</param>
        /// <returns>The final order decision.</returns>
        [HttpPost("submit")]
        [ProducesResponseType(typeof(SubmitOrderResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SubmitOrderResponse>> SubmitTradeRequest(SubmitOrderRequest request)
        {
            var result = await this.orderService.SubmitOrder(request);

            return this.Ok(result);
        }

        /// <summary>
        /// Gets order history with optional filtering by symbol and order status.
        /// </summary>
        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrdersHistory(
            [FromQuery] string? symbol,
            [FromQuery] OrderStatus? status)
        {
            var orders = await this.orderRepository.GetHistoryAsync(symbol, status);

            if (!orders.Any())
            {
                return this.NotFound($"No orders history found");
            }

            return this.Ok(orders);
        }

        /// <summary>
        /// Gets order history for a specific symbol.
        /// </summary>
        [HttpGet("{symbol}/history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOrdersHistoryBySymbol(string symbol)
        {
            var orders = await this.orderRepository.GetHistoryBySymbolAsync(symbol);

            if (!orders.Any())
            {
                return this.NotFound($"There is no history of orders for {symbol} ");
            }

            return this.Ok(orders);
        }
    }
}