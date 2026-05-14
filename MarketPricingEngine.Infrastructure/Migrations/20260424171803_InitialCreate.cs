using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPricingEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "latest_prices",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    symbol = table.Column<string>(type: "TEXT", nullable: false),
                    bid_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ask_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    current_market_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    spread = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    spread_percent = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_latest_prices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    external_order_id = table.Column<string>(type: "TEXT", nullable: false),
                    symbol = table.Column<string>(type: "TEXT", nullable: false),
                    side = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    requested_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    notional_amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    source = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false),
                    rejection_reason = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "price_ticks",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    symbol = table.Column<string>(type: "TEXT", nullable: false),
                    bid_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ask_price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    mid_price = table.Column<decimal>(type: "TEXT", nullable: false),
                    spread = table.Column<decimal>(type: "TEXT", nullable: false),
                    spread_percent = table.Column<decimal>(type: "TEXT", nullable: false),
                    timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_ticks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trading_rules",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    max_notional_amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    max_quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    max_price_deviation_percent = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    reject_duplicate_order_ids = table.Column<bool>(type: "INTEGER", nullable: false),
                    enable_symbol_whitelist = table.Column<bool>(type: "INTEGER", nullable: false),
                    allowed_symbols_csv = table.Column<string>(type: "TEXT", nullable: false),
                    auto_trading_spread_threshold_percent = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_rules", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_latest_prices_symbol",
                table: "latest_prices",
                column: "symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_external_order_id",
                table: "orders",
                column: "external_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_symbol",
                table: "orders",
                column: "symbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "latest_prices");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "price_ticks");

            migrationBuilder.DropTable(
                name: "trading_rules");
        }
    }
}
