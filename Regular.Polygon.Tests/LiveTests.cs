using Xunit;

namespace Regular.Polygon.Tests;

public class LiveTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task GetStockLiveAggregatesBySecond(PolygonApi client)
	{
		var cnt = 0;
		await foreach (var _ in client.GetStockLiveAggregatesBySecond("SPY"))
		{
			if (++cnt >= 3)
				break;
		}
	}
}
