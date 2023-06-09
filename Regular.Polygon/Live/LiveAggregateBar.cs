using System.Text.Json.Serialization;

namespace Regular.Polygon.Live;

/// <summary>
/// Represents the aggregate activity during a time period
/// </summary>
/// <param name="EventType">The type of the event</param>
/// <param name="Symbol">The symbol of the security.</param>
/// <param name="Volume">The trading volume during the aggregate time period.</param>
/// <param name="DayAccumulatedVolume">Today's accumulated volume.</param>
/// <param name="DayOpenPrice">Today's accumulated volume.</param>
/// <param name="DayAveragePrice">Today's volume weighted average price.</param>
/// <param name="AveragePrice">The weighted average price during the aggregate time period.</param>
/// <param name="Open">The opening price of the aggregate time period.</param>
/// <param name="High">The highest price during the aggregate time period.</param>
/// <param name="Low">The highest price during the aggregate time period.</param>
/// <param name="Close">The closing price of the aggregate time period.</param>
/// <param name="AverageTradeSize">The average trade size during the aggregate time period.</param>
/// <param name="Start">The starting timestamp of the aggregate window.</param>
/// <param name="End">The ending timestamp of the aggregate window.</param>
public sealed record LiveAggregateBar(
	[property: JsonPropertyName("ev")]
	string EventType,
	[property: JsonPropertyName("sym")]
	string Symbol,
	[property: JsonPropertyName("v")]
	int Volume,
	[property: JsonPropertyName("av")]
	long DayAccumulatedVolume,
	[property: JsonPropertyName("op")]
	decimal? DayOpenPrice,
	[property: JsonPropertyName("a")]
	decimal DayAveragePrice,
	[property: JsonPropertyName("vw")]
	decimal AveragePrice,
	[property: JsonPropertyName("o")]
	decimal Open,
	[property: JsonPropertyName("h")]
	decimal High,
	[property: JsonPropertyName("l")]
	decimal Low,
	[property: JsonPropertyName("c")]
	decimal Close,
	[property: JsonPropertyName("z")]
	decimal AverageTradeSize,
	[property: JsonPropertyName("s"), JsonConverter(typeof(UnixMillisecondDateTimeOffsetConverter))]
	DateTimeOffset Start,
	[property: JsonPropertyName("e"), JsonConverter(typeof(UnixMillisecondDateTimeOffsetConverter))]
	DateTimeOffset End)
{
	/// <summary>
	/// Whether or not this aggregate is for an OTC ticker.
	/// </summary>
	[JsonPropertyName("otc")]
	public bool OverTheCounter { get; set; }
}
