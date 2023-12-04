using Xunit;

namespace Regular.Polygon.Tests;

public class DistributionTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetStockSplit(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetStockSplits(new() { Ticker = "AAPL", });

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetDividend(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetDividends(new() { Ticker = "AAPL", });
}
