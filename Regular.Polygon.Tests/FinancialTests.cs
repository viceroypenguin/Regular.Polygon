using Xunit;

namespace Regular.Polygon.Tests;

public class FinancialTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetFinancials(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetStockFinancials(new() { Ticker = "AAPL", IncludeSources = true, });
}
