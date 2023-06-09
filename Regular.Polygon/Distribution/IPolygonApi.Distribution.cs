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
	Task<PolygonResponse<IReadOnlyList<StockSplit>>> GetStockSplits(StockSplitRequest? request = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Get a list of historical cash dividends, including the ticker symbol, declaration date, ex-dividend date, record date, pay date, frequency, and amount.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Dividend api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The current trading status of the exchanges and overall financial markets</returns>
	Task<PolygonResponse<IReadOnlyList<Dividend>>> GetDividends(DividendRequest? request = null, CancellationToken cancellationToken = default);
}
