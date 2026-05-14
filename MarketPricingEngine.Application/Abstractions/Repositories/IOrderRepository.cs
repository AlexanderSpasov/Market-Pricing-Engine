using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Domain.Enums;

namespace MarketPricingEngine.Application.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderEntity entity);

        Task<bool> CheckIfExistsByExternalIdAsync(string externalOrderId);

        Task<List<OrderEntity>> GetHistoryBySymbolAsync(string symbol);

        Task<List<OrderEntity>> GetHistoryAsync(string? symbol, OrderStatus? status);
    }
}