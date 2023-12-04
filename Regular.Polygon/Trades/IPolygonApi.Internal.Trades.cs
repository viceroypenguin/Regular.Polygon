using Refit;
using Regular.Polygon.Trades;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{
	[Get("/v2/aggs/ticker/{ticker}/range/{multiplier}/{timespan}/{from}/{to}")]
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateOnly from,
		DateOnly to,
		[Query] AggregateRequest? request = null,
		CancellationToken cancellationToken = default);

	[Get("/v2/aggs/ticker/{ticker}/range/{multiplier}/{timespan}/{from}/{to}")]
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		long from,
		long to,
		[Query] AggregateRequest? request = null,
		CancellationToken cancellationToken = default);

	[Get("/v1/open-close/{ticker}/{date}")]
	Task<DailyPrice> GetDailyPrice(
		string ticker,
		DateOnly date,
		bool? adjusted = null,
		CancellationToken cancellationToken = default);

	[Get("/v2/aggs/ticker/{ticker}/prev")]
	Task<PolygonResponse<IReadOnlyList<AggregateBar>>> GetPreviousDailyPrice(
		string ticker,
		bool? adjusted = null,
		CancellationToken cancellationToken = default);

	[Get("/v3/trades/{ticker}")]
	Task<PolygonResponse<IReadOnlyList<Trade>>> GetTrades(
		string ticker,
		TradesRequest request,
		CancellationToken cancellationToken = default);

	[Get("/v3/trades/{ticker}")]
	Task<PolygonResponse<IReadOnlyList<Trade>>> GetTradesCursor(
		string ticker,
		[Query] string cursor,
		CancellationToken cancellationToken = default);
}
