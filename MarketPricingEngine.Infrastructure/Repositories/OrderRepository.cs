using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Domain.Entities;
using MarketPricingEngine.Domain.Enums;
using MarketPricingEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketPricingEngine.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MarketPricingDbContext context;

        public OrderRepository(MarketPricingDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(OrderEntity entity)
        {
            await this.context.Orders.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfExistsByExternalIdAsync(string externalOrderId)
            => await this.context.Orders
                .AnyAsync(x => x.ExternalOrderId == externalOrderId);

        public async Task<List<OrderEntity>> GetHistoryAsync(string? symbol, OrderStatus? status)
        {
            var query = this.context.Orders.AsQueryable();

            if(!string.IsNullOrWhiteSpace(symbol))
            {
                query.Where(x => x.Symbol == symbol);
            }

            if(status.HasValue)
            {
                query.Where(x => x.Status == status);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<OrderEntity>> GetHistoryBySymbolAsync(string symbol)
            => await this.context.Orders
                .Where(x => x.Symbol == symbol)
                .ToListAsync();
    }
}