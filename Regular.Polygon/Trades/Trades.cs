using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Refit;

namespace Regular.Polygon.Trades;

/// <summary>
/// Request class to hold parameters for the Trades api
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class TradesRequest
{
	/// <summary>
	/// Specify a timestamp. 
	/// </summary>
	[AliasAs("timestamp")]
	public DateTimeOffset? Timestamp { get; set; }

	/// <summary>
	/// Search trades after the given timestamp.
	/// </summary>
	[AliasAs("timestamp.gte")]
	public DateTimeOffset? TimestampGreaterThan { get; set; }

	/// <summary>
	/// Search trades before the given timestamp.
	/// </summary>
	[AliasAs("timestamp.lte")]
	public DateTimeOffset? TimestampLessThan { get; set; }

	/// <summary>
	/// Sort the results by <see cref="AggregateBar.Timestamp"/>.
	/// </summary>
	[AliasAs("order")]
	public SortOrder? Order { get; set; }

	/// <summary>
	/// Limit the number of results returned, default is 10 and max is 50,000.
	/// </summary>
	[AliasAs("limit")]
	public int? Limit { get; set; }
}

/// <summary>
/// Information about a single trade.
/// </summary>
/// <param name="Id">The Trade ID which uniquely identifies a trade. These are unique per combination of ticker,
/// exchange, and TRF. For example: A trade for AAPL executed on NYSE and a trade for AAPL executed on NASDAQ could
/// potentially have the same Trade ID.</param>
/// <param name="Exchange">The exchange ID. See <a
/// href="https://polygon.io/docs/stocks/get_v3_reference_exchanges">Exchanges</a> for Polygon.io's mapping of exchange
/// IDs.</param>
/// <param name="Size">The size of a trade (also known as volume).</param>
/// <param name="Price">The price of the trade. This is the actual dollar value per whole share of this trade.</param>
/// <param name="ParticipantTimestamp">The nanosecond accuracy Participant/Exchange Unix Timestamp. This is the
/// timestamp of when the trade was actually generated at the exchange.</param>
/// <param name="SipTimestamp">The nanosecond accuracy SIP Unix Timestamp. This is the timestamp of when the SIP
/// received this trade from the exchange which produced it.</param>
/// <param name="SequenceNumber">The sequence number represents the sequence in which trade events happened. These are
/// increasing and unique per ticker symbol, but will not always be sequential (e.g., 1, 2, 6, 9, 10, 11). Values reset
/// after each trading session/day.</param>
[ExcludeFromCodeCoverage]
public sealed record Trade(
	string Id,
	int Exchange,
	decimal Size,
	decimal Price,
	[property: JsonPropertyName("participant_timestamp"), JsonConverter(typeof(UnixNanosecondDateTimeOffsetConverter))]
	DateTimeOffset ParticipantTimestamp,
	[property: JsonPropertyName("sip_timestamp"), JsonConverter(typeof(UnixNanosecondDateTimeOffsetConverter))]
	DateTimeOffset SipTimestamp,
	[property: JsonPropertyName("sequence_number")]
	long SequenceNumber)
{
	/// <summary>
	/// The trade correction indicator.
	/// </summary>
	[ExcludeFromCodeCoverage] // hard to find one
	public int? Correction { get; set; }

	/// <summary>
	/// A list of condition codes.
	/// </summary>
	public IReadOnlyList<int>? Conditions { get; set; }

	/// <summary>
	/// There are 3 tapes which define which exchange the ticker is listed on. These are integers in our objects which represent the letter of the alphabet. Eg: 1 = A, 2 = B, 3 = C.
	/// Tape A is NYSE listed securities;
	/// Tape B is NYSE ARCA / NYSE American;
	/// Tape C is NASDAQ.
	/// </summary>
	public int? Tape { get; set; }

	/// <summary>
	/// The ID for the Trade Reporting Facility where the trade took place.
	/// </summary>
	[JsonPropertyName("trf_id")]
	public int? TrfId { get; set; }

	/// <summary>
	/// The ID for the Trade Reporting Facility where the trade took place.
	/// </summary>
	[JsonPropertyName("trf_timestamp")]
	[JsonConverter(typeof(UnixNanosecondDateTimeOffsetConverter))]
	public DateTimeOffset? TrfTimestamp { get; set; }
}
