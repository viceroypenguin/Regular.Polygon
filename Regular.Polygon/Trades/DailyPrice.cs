using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Regular.Polygon.Trades;

/// <summary>
/// The open, close and afterhours prices of a ticker on a certain date
/// </summary>
/// <param name="Status">The status of this request's response.</param>
/// <param name="Symbol">The exchange symbol that this item is traded under.</param>
/// <param name="From">The timestamp for the start of the aggregate window.</param>
/// <param name="Open">The open price for the symbol in the given time period.</param>
/// <param name="High">The highest price for the symbol in the given time period.</param>
/// <param name="Low">The lowest price for the symbol in the given time period.</param>
/// <param name="Close">The highest price for the symbol in the given time period.</param>
/// <param name="Volume">The volume weighted average price.</param>
[ExcludeFromCodeCoverage]
public record DailyPrice(
	string Status,
	string Symbol,
	DateOnly From,
	decimal Open,
	decimal High,
	decimal Low,
	decimal Close,
	decimal Volume)
{
	/// <summary>
	/// The close price of the ticker symbol in after hours trading.
	/// </summary>
	public decimal? AfterHours { get; set; }

	/// <summary>
	/// The open price of the ticker symbol in pre-market trading.
	/// </summary>
	public decimal? PreMarket { get; set; }

	/// <summary>
	/// Whether or not this aggregate is for an OTC ticker.
	/// </summary>
	[JsonPropertyName("otc")]
	public bool OverTheCounter { get; set; }
}
