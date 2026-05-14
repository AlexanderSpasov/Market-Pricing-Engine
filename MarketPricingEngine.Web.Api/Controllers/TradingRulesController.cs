using MarketPricingEngine.Application.Abstractions.Repositories;
using MarketPricingEngine.Application.Dtos.TradingRules;
using MarketPricingEngine.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketPricingEngine.Web.Api.Controllers
{
    /// <summary>
    /// Manages trading rules configuration.
    /// </summary>
    [ApiController]
    [Route("api/trading-rules")]
    public class TradingRulesController : ControllerBase
    {
        private readonly ITradingRulesRepository tradingRulesRepository;

        public TradingRulesController(ITradingRulesRepository tradingRulesRepository)
        {
            this.tradingRulesRepository = tradingRulesRepository;
        }

        /// <summary>
        /// Gets the current trading rules configuration.
        /// </summary>
        [HttpGet("current")]
        public async Task<ActionResult> GetCurrentTradingRules()
        {
            var currentTradingRules = await this.tradingRulesRepository.GetAsync();

            if (currentTradingRules == null)
            {
                return this.NotFound("No active Trading Rules found");
            }

            return this.Ok(currentTradingRules);
        }

        /// <summary>
        /// Updates or creates the current trading rules configuration.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TradingRulesEntity), StatusCodes.Status200OK)]
        public async Task<ActionResult<TradingRulesEntity>> UpdateTradingRules(UpdateTradingRulesRequest request)
        {
            var tradingRules = new TradingRulesEntity
            {
                MaxNotionalAmount = request.MaxNotionalAmount,
                MaxQuantity = request.MaxQuantity,
                MaxPriceDeviationPercent = request.MaxPriceDeviationPercent,
                RejectDuplicateOrderIds = request.RejectDuplicateOrderIds,
                EnableSymbolWhitelist = request.EnableSymbolWhitelist,
                AllowedSymbolsCsv = request.AllowedSymbolsCsv,
                AutoTradingSpreadThresholdPercent = request.AutoTradingSpreadThresholdPercent,
                UpdatedAt = DateTime.UtcNow
            };

            await this.tradingRulesRepository.UpsertAsync(tradingRules);

            return this.Ok(tradingRules);
        }
    }
}