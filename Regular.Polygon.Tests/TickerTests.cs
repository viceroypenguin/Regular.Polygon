using CommunityToolkit.Diagnostics;
using Xunit;

namespace Regular.Polygon.Tests;

public class TickerTests : IClassFixture<PolygonFixture>
{
	private readonly PolygonFixture _fixture;

	public TickerTests(PolygonFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact]
	public async Task GetTickerTypesNullFilter()
	{
		// all we're looking for is successful api query
		var types = await _fixture.Client.GetTickerTypes();
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}

	[Fact]
	public async Task GetTickerTypesLocaleFilter()
	{
		// all we're looking for is successful api query
		var types = await _fixture.Client.GetTickerTypes(new() { Locale = Ticker.Locale.Us, });
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}

	[Fact]
	public async Task GetTickerTypesAssetClassFilter()
	{
		// all we're looking for is successful api query
		var types = await _fixture.Client.GetTickerTypes(new() { AssetClass = Ticker.AssetClass.Stocks, });
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}
}
