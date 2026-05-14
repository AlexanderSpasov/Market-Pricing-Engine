using MarketPricingEngine.Domain.Entities;

namespace MarketPricingEngine.Application.Abstractions.Repositories
{
    public interface IPriceTickRepository
    {
        Task AddAsync(PriceTickEntity entity);
    }
}