using Regular.Polygon.Ticker;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	/// <summary>
	/// List all ticker types that Polygon.io has.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Types api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of supported ticker types matching the parameters.</returns>
	[Get("/v3/reference/tickers/types")]
	Task<PolygonResponse<IReadOnlyList<TickerType>>> GetTickerTypes([Query] TickerTypeRequest? request = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Query all ticker symbols which are supported by Polygon.io. This API currently includes Stocks/Equities, Indices, Forex, and Crypto.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of tickers matching the query.</returns>
	[Get("/v3/reference/tickers")]
	Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickers([Query] TickerSearchRequest? request = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieve a subsequent page for a <see cref="SearchTickers(TickerSearchRequest?,CancellationToken)"/> response
	/// that had additional information.
	/// </summary>
	/// <param name="cursor">The cursor provided by polygon.io for the next page</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The next page of tickers matching the original query.</returns>
	[Get("/v3/reference/tickers")]
	Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickersCursor([Query] string cursor, CancellationToken cancellationToken = default);

	/// <summary>
	/// Query all ticker symbols which are supported by Polygon.io. This API currently includes Stocks/Equities, Indices, Forex, and Crypto.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of tickers matching the query.</returns>
	public Task<IReadOnlyList<TickerSearchResult>> SearchTickersAll(TickerSearchRequest? request = null, CancellationToken cancellationToken = default) =>
		GetFullList(SearchTickers(request, cancellationToken), SearchTickersCursor, cancellationToken);

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
	[Get("/v3/reference/tickers/{ticker}")]
	Task<PolygonResponse<TickerDetail>> GetTickerDetails(string ticker, [Query] DateOnly? date = null, CancellationToken cancellationToken = default);
}
