using Regular.Polygon.Trades;
using Xunit;

namespace Regular.Polygon.Tests;

public class TradeTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetAggregatesDate(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetAggregateBars(
			"AAPL",
			1,
			Timespan.Day,
			new DateOnly(1990, 1, 1),
			new DateOnly(2023, 1, 1),
			new() { Limit = 5000, });

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetAggregatesOtc(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetAggregateBars(
			"TCEHY",
			1,
			Timespan.Day,
			new DateOnly(1990, 1, 1),
			new DateOnly(2023, 1, 1),
			new() { Limit = 5000, });

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetAggregatesDateTimeOffset(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetAggregateBars(
			"AAPL",
			1,
			Timespan.Day,
			new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
			new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
			new() { Limit = 5000, });

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetDailyPrice(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetDailyPrice(
			"AAPL",
			new DateOnly(2023, 1, 3));

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetDailyPriceOtc(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetDailyPrice(
			"TCEHY",
			new DateOnly(2023, 1, 3));

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetPreviousDailyPrice(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetPreviousDailyPrice(
			"AAPL");

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetTrades(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetTradesAll(
			"AAPL",
			new()
			{
				TimestampGreaterThan = new DateTimeOffset(2023, 1, 3, 13, 30, 0, TimeSpan.Zero),
				TimestampLessThan = new DateTimeOffset(2023, 1, 3, 14, 30, 0, TimeSpan.Zero),
				Limit = 5_000,
			});
}
