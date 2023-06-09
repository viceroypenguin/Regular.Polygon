using Refit;
using Regular.Polygon.Trades;

namespace Regular.Polygon;

internal partial class PolygonApi
{
	public Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateTimeOffset from,
		DateTimeOffset to,
		AggregateRequest? request = null,
		CancellationToken cancellationToken = default) =>
		GetAggregateBars(
			ticker,
			multiplier,
			timespan,
			from.ToUnixTimeMilliseconds(),
			to.ToUnixTimeMilliseconds(),
			request,
			cancellationToken);

	public Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateOnly from,
		DateOnly to,
		AggregateRequest? request = null,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetAggregateBars(ticker, multiplier, timespan, from, to, request, cancellationToken);

	public Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		long from,
		long to,
		AggregateRequest? request = null,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetAggregateBars(ticker, multiplier, timespan, from, to, request, cancellationToken);

	public Task<DailyPrice> GetDailyPrice(
		string ticker,
		DateOnly date,
		bool? adjusted = null,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetDailyPrice(ticker, date, adjusted, cancellationToken);

	public Task<PolygonResponse<IReadOnlyList<AggregateBar>>> GetPreviousDailyPrice(
		string ticker,
		bool? adjusted = null,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetPreviousDailyPrice(ticker, adjusted, cancellationToken);

	public Task<PolygonResponse<IReadOnlyList<Trade>>> GetTrades(
		string ticker,
		TradesRequest request,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetTrades(ticker, request, cancellationToken);

	public Task<PolygonResponse<IReadOnlyList<Trade>>> GetTradesCursor(
		string ticker,
		string cursor,
		CancellationToken cancellationToken = default) =>
		_refitApi.GetTradesCursor(ticker, cursor, cancellationToken);

	public Task<IReadOnlyList<Trade>> GetTradesAll(
		string ticker,
		TradesRequest request,
		CancellationToken cancellationToken = default) =>
		GetFullList(
			GetTrades(ticker, request, cancellationToken),
			(c, ct) => GetTradesCursor(ticker, c, ct),
			cancellationToken);
}

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
