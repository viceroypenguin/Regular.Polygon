using Regular.Polygon.Market;

namespace Regular.Polygon;

public partial class PolygonApi
{
	/// <summary>
	/// Get the current trading status of the exchanges and overall financial markets.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The current trading status of the exchanges and overall financial markets</returns>
	public Task<MarketStatus> GetMarketStatus(CancellationToken cancellationToken = default) =>
		_refitApi.GetMarketStatus(cancellationToken);

	/// <summary>
	/// Get upcoming market holidays and their open/close times.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the upcoming market holidays</returns>
	public Task<IReadOnlyList<MarketHoliday>> GetMarketHolidays(CancellationToken cancellationToken = default) =>
		_refitApi.GetMarketHolidays(cancellationToken);
}
