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
	public Task GetAggregates() =>
		// all we're looking for is successful api query
		_fixture.Client.GetAggregateBars(
			"AAPL",
			1,
			Timespan.Day,
			new DateOnly(1990, 1, 1),
			new DateOnly(2023, 1, 1),
			new() { Limit = 5000, });

	[Fact]
	public Task GetDailyPrice() =>
		// all we're looking for is successful api query
		_fixture.Client.GetDailyPrice(
			"AAPL",
			new DateOnly(2023, 1, 3));

	[Fact]
	public Task GetPreviousDailyPrice() =>
		// all we're looking for is successful api query
		_fixture.Client.GetPreviousDailyPrice(
			"AAPL");
}
