using Xunit;

namespace Regular.Polygon.Tests;

public class MarketTests : IClassFixture<PolygonFixture>
{
	private readonly PolygonFixture _fixture;

	public MarketTests(PolygonFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public Task GetMarketStatus() =>
		// all we're looking for is successful api query
		_fixture.Client.GetMarketStatus();

	[Fact]
	public Task GetMarketHolidays() =>
		// all we're looking for is successful api query
		_fixture.Client.GetMarketHolidays();
}
