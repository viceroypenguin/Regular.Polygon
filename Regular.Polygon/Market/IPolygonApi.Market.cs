using Regular.Polygon.Market;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	[Get("/v1/marketstatus/now")]
	Task<MarketStatus> GetMarketStatus();

	[Get("/v1/marketstatus/upcoming")]
	Task<IReadOnlyList<MarketHoliday>> GetMarketHolidays();
}
