using Xunit;

namespace Regular.Polygon.Tests;

public sealed class MarketTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetMarketStatus(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetMarketStatus();

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetMarketHolidays(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetMarketHolidays();
}
