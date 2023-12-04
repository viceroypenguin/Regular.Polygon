using Regular.Polygon.Financials;

namespace Regular.Polygon;

public partial class PolygonApi
{
	/// <summary>
	/// Get historical financial data for a stock ticker. The financials data is extracted from XBRL from company SEC
	/// filings using the methodology outlined <a
	/// href="http://xbrl.squarespace.com/understanding-sec-xbrl-financi/">here</a>.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Stock Financials api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of company SEC filings.</returns>
	/// <remarks>This API is experimental</remarks>
	public Task<PolygonResponse<IReadOnlyList<FinancialsFiling>>> GetStockFinancials(FinancialsRequest request, CancellationToken cancellationToken = default) =>
		_refitApi.GetStockFinancials(request, cancellationToken);
}
