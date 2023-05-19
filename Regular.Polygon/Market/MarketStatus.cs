namespace Regular.Polygon.Market;

public record MarketStatus(
	string Market,
	DateTimeOffset ServerTime,
	bool EarlyHours,
	bool AfterHours,
	IReadOnlyDictionary<string, string> Currencies,
	IReadOnlyDictionary<string, string> Exchanges);
