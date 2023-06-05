using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Regular.Polygon.Trades;

/// <summary>
/// The size of the time window.
/// </summary>
public enum Timespan
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "minute")] Minute,
	[EnumMember(Value = "hour")] Hour,
	[EnumMember(Value = "day")] Day,
	[EnumMember(Value = "week")] Week,
	[EnumMember(Value = "month")] Month,
	[EnumMember(Value = "quarter")] Quarter,
	[EnumMember(Value = "year")] Year,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Request class to hold additional parameters for the Aggregate Bars api
/// </summary>
public class AggregateRequest
{
	/// <summary>
	/// Whether or not the results are adjusted for splits.
	/// </summary>
	/// <remarks>
	/// By default, results are adjusted. Set this to <see langword="false"/> to get results that are not adjusted for
	/// splits.
	/// </remarks>
	[AliasAs("adjusted")]
	public bool? Adjusted { get; set; }

	/// <summary>
	/// Sort the results by <see cref="AggregateBar.Timestamp"/>.
	/// </summary>
	[AliasAs("order")]
	public SortOrder? Order { get; set; }

	/// <summary>
	/// Limit the number of results returned, default is 100 and max is 1000.
	/// </summary>
	/// <remarks>
	/// Max 50,000 and Default 5,000. Read more about how limit is used to calculate aggregate results in our article on
	/// <a href="https://polygon.io/blog/aggs-api-updates/">Aggregate Data API Improvements</a>.
	/// </remarks>
	[AliasAs("limit")]
	public int? Limit { get; set; }
}

/// <summary>
/// A response object containing request and status information for aggregate queries.
/// </summary>
public record AggregateResponse : PolygonResponse<IReadOnlyList<AggregateBar>>
{
	/// <summary>
	/// Whether or not this response was adjusted for splits.
	/// </summary>
	public required bool Adjusted { get; set; }

	/// <summary>
	/// The exchange symbol that this item is traded under.
	/// </summary>
	public required string Ticker { get; set; }

	/// <summary>
	/// The number of aggregates (minute or day) used to generate the response.
	/// </summary>
	public required int QueryCount { get; set; }

	/// <summary>
	/// The total number of results for this request.
	/// </summary>
	private int ResultsCount { set => Count = value; }
}

/// <summary>
/// A single OHLC aggregate result.
/// </summary>
/// <param name="Timestamp">The timestamp for the start of the aggregate window.</param>
/// <param name="Open">The open price for the symbol in the given time period.</param>
/// <param name="High">The highest price for the symbol in the given time period.</param>
/// <param name="Low">The lowest price for the symbol in the given time period.</param>
/// <param name="Close">The highest price for the symbol in the given time period.</param>
/// <param name="Volume">The volume weighted average price.</param>
public record AggregateBar(
	[property: JsonPropertyName("t"), JsonConverter(typeof(UnixMillisecondDateTimeOffsetConverter))]
	DateTimeOffset Timestamp,
	[property: JsonPropertyName("o")]
	decimal Open,
	[property: JsonPropertyName("h")]
	decimal High,
	[property: JsonPropertyName("l")]
	decimal Low,
	[property: JsonPropertyName("c")]
	decimal Close,
	[property: JsonPropertyName("v")]
	decimal Volume)
{
	/// <summary>
	/// The exchange symbol that this item is traded under.
	/// </summary>
	[JsonPropertyName("T")]
	public string? Ticker { get; set; }

	/// <summary>
	/// The number of transactions in the aggregate window.
	/// </summary>
	[JsonPropertyName("n")]
	public int? TransactionCount { get; set; }

	/// <summary>
	/// Whether or not this aggregate is for an OTC ticker.
	/// </summary>
	[JsonPropertyName("otc")]
	public bool OverTheCounter { get; set; }

	/// <summary>
	/// The volume weighted average price.
	/// </summary>
	[JsonPropertyName("vw")]
	public decimal? VolumeWeightedPrice { get; set; }
}
