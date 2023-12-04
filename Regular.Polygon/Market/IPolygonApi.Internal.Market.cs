using Refit;
using Regular.Polygon.Market;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{
	[Get("/v1/marketstatus/now")]
	Task<MarketStatus> GetMarketStatus(CancellationToken cancellationToken = default);

	[Get("/v1/marketstatus/upcoming")]
	Task<IReadOnlyList<MarketHoliday>> GetMarketHolidays(CancellationToken cancellationToken = default);
}
