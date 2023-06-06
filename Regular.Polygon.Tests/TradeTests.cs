using Regular.Polygon.Trades;
using Xunit;

namespace Regular.Polygon.Tests;

public class TradeTests : IClassFixture<PolygonFixture>
{
	private readonly PolygonFixture _fixture;

	public TradeTests(PolygonFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public Task GetAggregatesDate() =>
		// all we're looking for is successful api query
		_fixture.Client.GetAggregateBars(
			"AAPL",
			1,
			Timespan.Day,
			new DateOnly(1990, 1, 1),
			new DateOnly(2023, 1, 1),
			new() { Limit = 5000, });

	[Fact]
	public Task GetAggregatesOtc() =>
		// all we're looking for is successful api query
		_fixture.Client.GetAggregateBars(
			"TCEHY",
			1,
			Timespan.Day,
			new DateOnly(1990, 1, 1),
			new DateOnly(2023, 1, 1),
			new() { Limit = 5000, });

	[Fact]
	public Task GetAggregatesDateTimeOffset() =>
		// all we're looking for is successful api query
		_fixture.Client.GetAggregateBars(
			"AAPL",
			1,
			Timespan.Day,
			new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
			new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
			new() { Limit = 5000, });

	[Fact]
	public Task GetDailyPrice() =>
		// all we're looking for is successful api query
		_fixture.Client.GetDailyPrice(
			"AAPL",
			new DateOnly(2023, 1, 3));

	[Fact]
	public Task GetDailyPriceOtc() =>
		// all we're looking for is successful api query
		_fixture.Client.GetDailyPrice(
			"TCEHY",
			new DateOnly(2023, 1, 3));

	[Fact]
	public Task GetPreviousDailyPrice() =>
		// all we're looking for is successful api query
		_fixture.Client.GetPreviousDailyPrice(
			"AAPL");

	[Fact]
	public Task GetTrades() =>
		// all we're looking for is successful api query
		_fixture.Client.GetTradesAll(
			"AAPL",
			new()
			{
				TimestampGreaterThan = new DateTimeOffset(2023, 1, 3, 13, 30, 0, TimeSpan.Zero),
				TimestampLessThan = new DateTimeOffset(2023, 1, 3, 14, 30, 0, TimeSpan.Zero),
				Limit = 5_000,
			});
}
