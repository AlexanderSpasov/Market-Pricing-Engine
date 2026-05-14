using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketPricingEngine.Web.Api.Controllers
{
    /// <summary>
    /// Provides access to latest market prices.
    /// </summary>
    [ApiController]
    [Route("api/prices")]
    public class PricesController : ControllerBase
    {
        private readonly ILatestPriceRepository latestPriceRepository;

        public PricesController(ILatestPriceRepository latestPriceRepository)
        {
            this.latestPriceRepository = latestPriceRepository;
        }

        /// <summary>
        /// Gets the latest known market price for a symbol.
        /// </summary>
        [HttpGet("{symbol}/latest")]
        [ProducesResponseType(typeof(LatestPriceEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LatestPriceEntity>> GetLatestPriceBySymbol(string symbol)
        {
            var latestPrice = await this.latestPriceRepository.GetLatestPriceBySymbol(symbol);

            if (latestPrice == null)
            {
                return this.NotFound($"No latest price found for symbol '{symbol}'.");
            }

            return this.Ok(latestPrice);
        }
    }
}