using CommunityToolkit.Diagnostics;
using Regular.Polygon.Ticker;
using Xunit;

namespace Regular.Polygon.Tests;

public sealed class TickerTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetTickerTypesNullFilter(PolygonApi client)
	{
		// all we're looking for is successful api query
		var types = await client.GetTickerTypes();
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetTickerTypesLocaleFilter(PolygonApi client)
	{
		// all we're looking for is successful api query
		var types = await client.GetTickerTypes(new() { Locale = Ticker.Locale.Us, });
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetTickerTypesAssetClassFilter(PolygonApi client)
	{
		// all we're looking for is successful api query
		var types = await client.GetTickerTypes(new() { AssetClass = Ticker.AssetClass.Stocks, });
		Guard.IsGreaterThan(types.Count ?? 0, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task SearchTickers(PolygonApi client)
	{
		// all we're looking for is successful api query
		var tickers = await client.SearchTickersAll(new() { Ticker = "MSFT", });
		Guard.IsGreaterThan(tickers.Count, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task SearchTickersAll(PolygonApi client)
	{
		// all we're looking for is successful api query
		var tickers = await client.SearchTickersAll(new() { TickerType = "ETF", Market = AssetClass.Stocks, Limit = 1000, });
		Guard.IsGreaterThan(tickers.Count, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task SearchDelistedTickers(PolygonApi client)
	{
		// all we're looking for is successful api query
		var tickers = await client.SearchTickers(new() { Ticker = "TA", Active = false, });
		Guard.IsGreaterThan(tickers.Count ?? 0, 0);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetTickerDetails(PolygonApi client)
	{
		// all we're looking for is successful api query
		var details = await client.GetTickerDetails(ticker: "MSFT");
		Assert.Equal("MSFT", details.Results!.Ticker);
	}

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetDelistedTickerDetails(PolygonApi client)
	{
		// all we're looking for is successful api query
		var details = await client.GetTickerDetails(ticker: "TA", date: new DateOnly(2023, 05, 01));
		Assert.Equal("TA", details.Results!.Ticker);
	}
}
