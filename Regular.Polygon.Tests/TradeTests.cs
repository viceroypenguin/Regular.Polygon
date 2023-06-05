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
			DateOnly.FromDateTime(DateTime.Today),
			new() { Limit = 5000, });
}
