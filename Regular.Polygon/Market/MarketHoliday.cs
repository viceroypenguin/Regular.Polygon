using System.Diagnostics.CodeAnalysis;

namespace Regular.Polygon.Market;

/// <summary>
/// An upcoming market holiday.
/// </summary>
/// <param name="Date">The date of the holiday.</param>
/// <param name="Exchange">Which market the record is for.</param>
/// <param name="Name">Which market the record is for.</param>
/// <param name="Status">The status of the market on the holiday.</param>
/// <param name="Open">The market open time on the holiday (if it's not closed).</param>
/// <param name="Close">The market close time on the holiday (if it's not closed).</param>
[ExcludeFromCodeCoverage]
public record MarketHoliday(
	DateOnly Date,
	string Exchange,
	string Name,
	string Status,
	DateTimeOffset? Open,
	DateTimeOffset? Close);
