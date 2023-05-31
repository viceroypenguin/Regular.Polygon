using Regular.Polygon.Distribution;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	/// <summary>
	/// Get a list of historical stock splits, including the ticker symbol, the execution date, and the factors of the split ratio.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Stock Split api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The current trading status of the exchanges and overall financial markets</returns>
	[Get("/v3/reference/splits")]
	Task<PolygonResponse<IReadOnlyList<StockSplit>>> GetStockSplits([Query] StockSplitRequest? request = null, CancellationToken cancellationToken = default);
}
