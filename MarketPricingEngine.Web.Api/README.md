# MarketPricingEngine

## Overview

MarketPricingEngine is a .NET 9 backend application that simulates a market data feed and processes trading logic.

The system continuously generates price updates for multiple symbols, maintains the latest market state, and automatically creates trading orders based on price movement and configurable trading rules.

---

## Project Goal

The goal of this project is to simulate a simplified trading backend system that demonstrates:

- Real-time price generation
- Market data processing
- Trading rule validation
- Background processing
- REST API design
- Clean architecture principles

---

## Architecture

The project follows a layered structure:

- **Domain**  
  Core business models and rules.

- **Application**  
  Interfaces, DTOs, and service contracts.

- **Infrastructure**  
  Implementations (EF Core, services, background processing).

- **Web.Api**  
  HTTP layer and application entry point.

---

## Data Flow

The system processes data in the following pipeline:

```text
BackgroundService
→ PriceFeedService
→ MarketPriceGenerator
→ PriceTickRepository
→ LatestPriceRepository
→ AutoTradingService
→ TradingRulesEvaluator
→ OrderRepository
```

---

## Persistence Strategy

- PriceTicks are stored as historical records
- LatestPrices store only the current state per symbol
- Orders are stored independently

This allows both historical analysis and fast access to current data.

---

## Auto Trading Logic

The system compares previous and current market price:

- If price increases → Sell
- If price decreases → Buy

A fixed 0.03% adjustment is applied for simulation purposes.

---

## Notes

- The application uses SQLite for simplicity and portability.
- No external database setup is required.
- A background worker starts automatically and continuously generates market data.
- Trading rules must be configured via API before auto-trading becomes active.

---

## How to Run

1. Restore dependencies
```bash
dotnet restore
```

2. Apply database migrations
```bash
dotnet ef database update \
--project MarketPricingEngine.Infrastructure \
--startup-project MarketPricingEngine.Web.Api \
--context MarketPricingDbContext
```

3. Run the application
```bash
dotnet run --project MarketPricingEngine.Web.Api
```

4. Open Swagger
https://localhost:<port>/swagger