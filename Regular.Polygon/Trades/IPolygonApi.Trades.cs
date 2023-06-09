using Regular.Polygon.Trades;

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
		AggregateRequest? request = null,
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
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		DateOnly from,
		DateOnly to,
		AggregateRequest? request = null,
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
	Task<AggregateResponse> GetAggregateBars(
		string ticker,
		int multiplier,
		Timespan timespan,
		long from,
		long to,
		AggregateRequest? request = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get the open, close and afterhours prices of a ticker on a certain date.
	/// </summary>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="date">The date of the requested daily prices.</param>
	/// <param name="adjusted">Whether or not the results are adjusted for splits. By default, results are adjusted. Set
	/// this to <see langword="false"/> to get results that are NOT adjusted for splits.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The open, close and afterhours prices of a ticker on a certain date.</returns>
	Task<DailyPrice> GetDailyPrice(
		string ticker,
		DateOnly date,
		bool? adjusted = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get the open, close and afterhours prices of a ticker of the previous trading day.
	/// </summary>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="adjusted">Whether or not the results are adjusted for splits. By default, results are adjusted. Set
	/// this to <see langword="false"/> to get results that are NOT adjusted for splits.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>The open, close and afterhours prices of a ticker of the previous trading day.</returns>
	Task<PolygonResponse<IReadOnlyList<AggregateBar>>> GetPreviousDailyPrice(
		string ticker,
		bool? adjusted = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get trades for a ticker symbol in a given time range.
	/// </summary>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="request">Request object to hold additional parameters for the Trades api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the trades that occurred during the given time range.</returns>
	Task<PolygonResponse<IReadOnlyList<Trade>>> GetTrades(
		string ticker,
		TradesRequest request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get trades for a ticker symbol in a given time range.
	/// </summary>
	/// <param name="ticker">The ticker symbol of the security.</param>
	/// <param name="request">Request object to hold additional parameters for the Trades api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of the trades that occurred during the given time range.</returns>
	Task<IReadOnlyList<Trade>> GetTradesAll(
		string ticker,
		TradesRequest request,
		CancellationToken cancellationToken = default);
}
