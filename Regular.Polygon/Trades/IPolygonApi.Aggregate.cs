﻿using Regular.Polygon.Trades;

namespace Regular.Polygon;

public partial interface IPolygonApi
{
	/// <summary>
	/// Get aggregate bars for a stock over a given date range in custom time window sizes.
	/// </summary>
	/// <remarks>
	/// For example, if <paramref name="timespan"/> = <see cref="Timespan.Minute"/> and <paramref name="multiplier"/> =
	/// <c>5</c> then 5-minute bars will be returned.
	/// </remarks>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="multiplier">The size of the time window.</param>
	/// <param name="timespan">The size of the time window.</param>
	/// <param name="from">The start of the aggregate time window.</param>
	/// <param name="to">The end of the aggregate time window.</param>
	/// <param name="request">Request object to hold additional parameters for the Aggregates api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the OHLC bars for the time windows requested.</returns>
	/// <remarks>
	/// Read more about how aggregate results are calculated in our article on <a
	/// href="https://polygon.io/blog/aggs-api-updates/">Aggregate Data API Improvements</a>.
	/// </remarks>
	public Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateTimeOffset from,
		DateTimeOffset to,
		[Query] AggregateRequest? request = null,
		CancellationToken cancellationToken = default)
	{
		return GetAggregateBars(
			ticker,
			multiplier,
			timespan,
			from.ToUnixTimeMilliseconds(),
			to.ToUnixTimeMilliseconds(),
			request,
			cancellationToken);
	}

	/// <summary>
	/// Get aggregate bars for a stock over a given date range in custom time window sizes.
	/// </summary>
	/// <remarks>
	/// For example, if <paramref name="timespan"/> = <see cref="Timespan.Minute"/> and <paramref name="multiplier"/> =
	/// <c>5</c> then 5-minute bars will be returned.
	/// </remarks>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="multiplier">The size of the time window.</param>
	/// <param name="timespan">The size of the time window.</param>
	/// <param name="from">The start of the aggregate time window.</param>
	/// <param name="to">The end of the aggregate time window.</param>
	/// <param name="request">Request object to hold additional parameters for the Aggregates api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the OHLC bars for the time windows requested.</returns>
	/// <remarks>
	/// Read more about how aggregate results are calculated in our article on <a
	/// href="https://polygon.io/blog/aggs-api-updates/">Aggregate Data API Improvements</a>.
	/// </remarks>
	[Get("/v2/aggs/ticker/{ticker}/range/{multiplier}/{timespan}/{from}/{to}")]
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateOnly from,
		DateOnly to,
		[Query] AggregateRequest? request = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get aggregate bars for a stock over a given date range in custom time window sizes.
	/// </summary>
	/// <remarks>
	/// For example, if <paramref name="timespan"/> = <see cref="Timespan.Minute"/> and <paramref name="multiplier"/> =
	/// <c>5</c> then 5-minute bars will be returned.
	/// </remarks>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="multiplier">The size of the time window.</param>
	/// <param name="timespan">The size of the time window.</param>
	/// <param name="from">The start of the aggregate time window.</param>
	/// <param name="to">The end of the aggregate time window.</param>
	/// <param name="request">Request object to hold additional parameters for the Aggregates api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the OHLC bars for the time windows requested.</returns>
	/// <remarks>
	/// Read more about how aggregate results are calculated in our article on <a
	/// href="https://polygon.io/blog/aggs-api-updates/">Aggregate Data API Improvements</a>.
	/// </remarks>
	[Get("/v2/aggs/ticker/{ticker}/range/{multiplier}/{timespan}/{from}/{to}")]
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		long from,
		long to,
		[Query] AggregateRequest? request = null,
		CancellationToken cancellationToken = default);
}
