using Regular.Polygon.Market;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	/// <summary>
	/// Get the current trading status of the exchanges and overall financial markets.
	/// </summary>
	/// <returns></returns>
	[Get("/v1/marketstatus/now")]
	Task<MarketStatus> GetMarketStatus();

	/// <summary>
	/// Get upcoming market holidays and their open/close times.
	/// </summary>
	/// <returns></returns>
	[Get("/v1/marketstatus/upcoming")]
	Task<IReadOnlyList<MarketHoliday>> GetMarketHolidays();
}
