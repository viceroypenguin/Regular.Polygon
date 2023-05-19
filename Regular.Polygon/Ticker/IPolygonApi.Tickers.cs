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
}
