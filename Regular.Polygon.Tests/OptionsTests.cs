using CommunityToolkit.Diagnostics;
using Xunit;

namespace Regular.Polygon.Tests;

public sealed class OptionsTests
{
	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public Task GetOptionsContractDetail(PolygonApi client) =>
		// all we're looking for is successful api query
		client.GetOptionsContractDetail("O:SPY231222C00285000", new DateOnly(2023, 12, 06));

	[Theory]
	[MemberData(nameof(PolygonFixture.Data), MemberType = typeof(PolygonFixture))]
	public async Task SearchOptionsContracts(PolygonApi client)
	{
		// all we're looking for is successful api query
		var contracts = await client.SearchOptionsContractsAll(new()
		{
			UnderlyingTicker = "SPY",
			ContractType = "call",
			Limit = 100,
			ExpirationDateGreaterThan = DateOnly.FromDateTime(DateTime.Today).AddMonths(1),
			ExpirationDateLessThan = DateOnly.FromDateTime(DateTime.Today).AddMonths(1).AddDays(7),
		});
		Guard.IsGreaterThan(contracts.Count, 0);
	}
}
