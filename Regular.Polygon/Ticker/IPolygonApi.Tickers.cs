using Regular.Polygon.Ticker;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	/// <summary>
	/// List all ticker types that Polygon.io has.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Types api.</param>
	/// <returns>A list of supported ticker types matching the parameters.</returns>
	[Get("/v3/reference/tickers/types")]
	Task<PolygonResponse<IReadOnlyList<TickerType>>> GetTickerTypes([Query] TickerTypeRequest? request = null);

	/// <summary>
	/// Query all ticker symbols which are supported by Polygon.io. This API currently includes Stocks/Equities, Indices, Forex, and Crypto.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Ticker Search api.</param>
	/// <returns>A list of tickers matching the query.</returns>
	[Get("/v3/reference/tickers")]
	Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickers([Query] TickerSearchRequest? request = null);
}
