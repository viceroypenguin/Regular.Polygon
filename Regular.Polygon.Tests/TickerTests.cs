using CommunityToolkit.Diagnostics;
using Regular.Polygon.Ticker;
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

	[Fact]
	public async Task SearchTickers()
	{
		// all we're looking for is successful api query
		var tickers = await _fixture.Client.SearchTickers(new() { Ticker = "MSFT", });
		Guard.IsGreaterThan(tickers.Count ?? 0, 0);
	}

	[Fact]
	public async Task SearchTickersAll()
	{
		// all we're looking for is successful api query
		var tickers = await _fixture.Client.SearchTickersAll(new() { TickerType = "ETF", Market = AssetClass.Stocks, Limit = 1000, });
		Guard.IsGreaterThan(tickers.Count, 0);
	}

	[Fact]
	public async Task SearchDelistedTickers()
	{
		// all we're looking for is successful api query
		var tickers = await _fixture.Client.SearchTickers(new() { Ticker = "TA", Active = false, });
		Guard.IsGreaterThan(tickers.Count ?? 0, 0);
	}

	[Fact]
	public async Task GetTickerDetails()
	{
		// all we're looking for is successful api query
		var details = await _fixture.Client.GetTickerDetails(ticker: "MSFT");
		Assert.Equal("MSFT", details.Results!.Ticker);
	}

	[Fact]
	public async Task GetDelistedTickerDetails()
	{
		// all we're looking for is successful api query
		var details = await _fixture.Client.GetTickerDetails(ticker: "TA", date: new DateOnly(2023, 05, 01));
		Assert.Equal("TA", details.Results!.Ticker);
	}
}
