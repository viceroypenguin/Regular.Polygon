using System.Diagnostics.CodeAnalysis;

namespace Regular.Polygon.Market;

/// <summary>
/// Current trading status of the exchanges and financial markets
/// </summary>
/// <param name="Market">The status of the market as a whole.</param>
/// <param name="ServerTime">The current time of the server.</param>
/// <param name="EarlyHours">Whether or not the market is in pre-market hours.</param>
/// <param name="AfterHours">Whether or not the market is in post-market hours.</param>
/// <param name="Currencies">A collection of currency markets and their current status.</param>
/// <param name="Exchanges">A collection of security markets and their current status.</param>
[ExcludeFromCodeCoverage]
public record MarketStatus(
	string Market,
	DateTimeOffset ServerTime,
	bool EarlyHours,
	bool AfterHours,
	IReadOnlyDictionary<string, string> Currencies,
	IReadOnlyDictionary<string, string> Exchanges);
