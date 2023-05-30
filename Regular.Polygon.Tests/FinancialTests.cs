using CommunityToolkit.Diagnostics;
using Regular.Polygon.Ticker;
using Xunit;

namespace Regular.Polygon.Tests;

public class FinancialTests : IClassFixture<PolygonFixture>
{
	private readonly PolygonFixture _fixture;

	public FinancialTests(PolygonFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public Task GetFinancials() =>
		// all we're looking for is successful api query
		_fixture.Client.GetStockFinancials(new() { Ticker = "AAPL", });
}
