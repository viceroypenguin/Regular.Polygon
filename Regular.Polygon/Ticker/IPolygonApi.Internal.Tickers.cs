using Refit;
using Regular.Polygon.Ticker;

namespace Regular.Polygon;

internal partial class PolygonApi
{
	public Task<PolygonResponse<IReadOnlyList<TickerType>>> GetTickerTypes(TickerTypeRequest? request = null, CancellationToken cancellationToken = default) =>
		_refitApi.GetTickerTypes(request, cancellationToken);

	public Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickers(TickerSearchRequest? request = null, CancellationToken cancellationToken = default) =>
		_refitApi.SearchTickers(request, cancellationToken);

	public Task<PolygonResponse<IReadOnlyList<TickerSearchResult>>> SearchTickersCursor(string cursor, CancellationToken cancellationToken = default) =>
		_refitApi.SearchTickersCursor(cursor, cancellationToken);

	public Task<IReadOnlyList<TickerSearchResult>> SearchTickersAll(TickerSearchRequest? request = null, CancellationToken cancellationToken = default) =>
		GetFullList(SearchTickers(request, cancellationToken), SearchTickersCursor, cancellationToken);

	public Task<PolygonResponse<TickerDetail>> GetTickerDetails(string ticker, DateOnly? date = null, CancellationToken cancellationToken = default) =>
		_refitApi.GetTickerDetails(ticker, date, cancellationToken);
}

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
