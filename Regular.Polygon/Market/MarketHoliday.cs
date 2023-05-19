namespace Regular.Polygon.Market;

public record MarketHoliday(
	DateOnly Date,
	string Exchange,
	string Name,
	string Status);
