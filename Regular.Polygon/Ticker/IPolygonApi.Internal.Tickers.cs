using Refit;
using Regular.Polygon.Ticker;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{
	[Get("/v3/reference/tickers/types")]
	Task<PolygonResponse<IReadOnlyList<TickerType>>> GetTickerTypes([Query] TickerTypeRequest? request = null, CancellationToken cancellationToken = default);

	[Get("/v3/reference/tickers")]
	Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickers([Query] TickerSearchRequest? request = null, CancellationToken cancellationToken = default);

	[Get("/v3/reference/tickers")]
	Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickersCursor([Query] string cursor, CancellationToken cancellationToken = default);

	[Get("/v3/reference/tickers/{ticker}")]
	Task<PolygonResponse<TickerDetail>> GetTickerDetails(string ticker, [Query] DateOnly? date = null, CancellationToken cancellationToken = default);
}
