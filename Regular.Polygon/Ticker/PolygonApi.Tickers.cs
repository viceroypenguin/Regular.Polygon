using Regular.Polygon.Ticker;

namespace Regular.Polygon;

public partial class PolygonApi
{
	/// <summary>
	/// List all ticker types that Polygon.io has.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Types api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of supported ticker types matching the parameters.</returns>
	public Task<PolygonResponse<IReadOnlyList<TickerType>>> GetTickerTypes(TickerTypeRequest? request = null, CancellationToken cancellationToken = default) =>
		_refitApi.GetTickerTypes(request, cancellationToken);

	/// <summary>
	/// Query all ticker symbols which are supported by Polygon.io. This API currently includes Stocks/Equities, Indices, Forex, and Crypto.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of tickers matching the query.</returns>
	public Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickers(TickerSearchRequest? request = null, CancellationToken cancellationToken = default) =>
		_refitApi.SearchTickers(request, cancellationToken);

	/// <summary>
	/// Query all ticker symbols which are supported by Polygon.io. This API currently includes Stocks/Equities, Indices, Forex, and Crypto.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of tickers matching the query.</returns>
	public Task<IReadOnlyList<TickerSearchResult>> SearchTickersAll(TickerSearchRequest? request = null, CancellationToken cancellationToken = default)
	{
		return GetFullList(SearchTickers(request, cancellationToken), SearchTickersCursor, cancellationToken);

		Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickersCursor(string cursor, CancellationToken cancellationToken = default) =>
			_refitApi.SearchTickersCursor(cursor, cancellationToken);
	}

	/// <summary>
	/// Get a single ticker supported by Polygon.io. This response will have detailed information about the ticker and the company behind it.
	/// </summary>
	/// <param name="ticker">The ticker symbol of the asset.</param>
	/// <param name="date">
	/// <para>
	/// Specify a point in time to get information about the ticker available on that date. When retrieving information
	/// from SEC filings, we compare this date with the period of report date on the SEC filing.
	/// </para>
	/// <para>
	/// For example, consider an SEC filing submitted by AAPL on 2019-07-31, with a period of report date ending on
	/// 2019-06-29. That means that the filing was submitted on 2019-07-31, but the filing was created based on
	/// information from 2019-06-29. If you were to query for AAPL details on 2019-06-29, the ticker details would
	/// include information from the SEC filing.
	/// </para>
	/// <para>
	/// Defaults to the most recent available date.
	/// </para>
	/// </param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>Detailed information on a Ticker</returns>
	public Task<PolygonResponse<TickerDetail>> GetTickerDetails(string ticker, DateOnly? date = null, CancellationToken cancellationToken = default) =>
		_refitApi.GetTickerDetails(ticker, date, cancellationToken);
}
