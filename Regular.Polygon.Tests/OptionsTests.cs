using Xunit;

namespace Regular.Polygon.Tests;

public class OptionsTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetOptionsContractDetail(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetOptionsContractDetail("O:SPY231222C00285000", new DateOnly(2023, 12, 06));
}
