using Xunit;

namespace Regular.Polygon.Tests;

public class DistributionTests : IClassFixture<PolygonFixture>
{
	private readonly PolygonFixture _fixture;

	public DistributionTests(PolygonFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public Task GetStockSplit() =>
		// all we're looking for is successful api query
		_fixture.Client.GetStockSplits(new() { Ticker = "AAPL", });

}
