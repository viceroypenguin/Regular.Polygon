using System.Text.Json.Serialization;

namespace Regular.Polygon.Live;

/// <summary>
/// Which tape recorded a particular trade
/// </summary>
public enum LiveTradeTape
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None = 0,
	Nyse = 1,
	Amex = 2,
	Nasdaq = 3,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Represents a single trade
/// </summary>
/// <param name="EventType">The type of the event</param>
/// <param name="Symbol">The symbol of the security.</param>
/// <param name="ExchangeId">The exchange ID. See <a href="https://polygon.io/docs/stocks/get_v3_reference_exchanges">Exchanges</a> for Polygon.io's mapping of exchange IDs.</param>
/// <param name="TradeId">The unique ID of the trade.</param>
/// <param name="Tape">The tape on which this trade is recorded</param>
/// <param name="Price">The trade price.</param>
/// <param name="TradeSize">The trade size.</param>
/// <param name="Conditions">The trade conditions. See <a href="https://polygon.io/glossary/us/stocks/conditions-indicators">Conditions and Indicators</a> for Polygon.io's trade conditions glossary.</param>
/// <param name="Timestamp">The trade timestamp</param>
/// <param name="SequenceNumber">The sequence number represents the sequence in which message events happened. These are increasing and unique per ticker symbol, but will not always be sequential (e.g., 1, 2, 6, 9, 10, 11).</param>
public sealed record LiveTrade(
	[property: JsonPropertyName("ev")]
	string EventType,
	[property: JsonPropertyName("sym")]
	string Symbol,
	[property: JsonPropertyName("x")]
	int ExchangeId,
	[property: JsonPropertyName("i")]
	string TradeId,
	[property: JsonPropertyName("z")]
	LiveTradeTape Tape,
	[property: JsonPropertyName("p")]
	decimal Price,
	[property: JsonPropertyName("s")]
	int TradeSize,
	[property: JsonPropertyName("c")]
	IReadOnlyList<int>? Conditions,
	[property: JsonPropertyName("t"), JsonConverter(typeof(UnixMillisecondDateTimeOffsetConverter))]
	DateTimeOffset Timestamp,
	[property: JsonPropertyName("q")]
	int SequenceNumber)
{
	/// <summary>
	/// The ID for the Trade Reporting Facility where the trade took place.
	/// </summary>
	[JsonPropertyName("trfi")]
	public int? TradeReportingFacility { get; set; }

	/// <summary>
	/// The TRF (Trade Reporting Facility) Timestamp. This is the timestamp of when the trade reporting facility received this trade.
	/// </summary>
	[JsonPropertyName("trft")]
	[JsonConverter(typeof(UnixMillisecondDateTimeOffsetConverter))]
	public DateTimeOffset? TradeReportingFacilityTimestamp { get; set; }
}
