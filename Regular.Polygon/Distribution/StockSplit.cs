using System.Text.Json.Serialization;
using Refit;

namespace Regular.Polygon.Distribution;

/// <summary>
/// Request class to hold parameters for the Stock Split api
/// </summary>
public class StockSplitRequest
{
	/// <summary>
	/// Specify a ticker symbol. 
	/// </summary>
	[AliasAs("ticker")]
	public string? Ticker { get; set; }

	/// <summary>
	/// Search ticker symbol string comparison greater than.
	/// </summary>
	[AliasAs("ticker.gte")]
	public string? TickerGreaterThan { get; set; }

	/// <summary>
	/// Search ticker symbol string comparison less than.
	/// </summary>
	[AliasAs("ticker.lte")]
	public string? TickerLessThan { get; set; }

	/// <summary>
	/// Query for splits on the execution date
	/// </summary>
	[AliasAs("execution_date")]
	public DateOnly? ExecutionDate { get; set; }

	/// <summary>
	/// Query for splits before the execution date
	/// </summary>
	[AliasAs("execution_date.lte")]
	public DateOnly? ExecutionDateBefore { get; set; }

	/// <summary>
	/// Query for splits after the execution date
	/// </summary>
	[AliasAs("execution_date.gte")]
	public DateOnly? ExecutionDateAfter { get; set; }

	/// <summary>
	/// Query for reverse stock splits. A split ratio where <see cref="StockSplit.SplitFrom"/> is greater than <see
	/// cref="StockSplit.SplitTo"/> represents a reverse split. By default this filter is not used.
	/// </summary>
	[AliasAs("reverse_split")]
	public bool? ReverseSplit { get; set; }

	/// <summary>
	/// Sort field used for ordering.
	/// </summary>
	[AliasAs("sort")]
	public string? Sort { get; set; }

	/// <summary>
	/// Order results based on the <see cref="Sort"/> field.
	/// </summary>
	[AliasAs("order")]
	public SortOrder? Order { get; set; }

	/// <summary>
	/// Limit the number of results returned, default is 100 and max is 1000.
	/// </summary>
	[AliasAs("limit")]
	public int? Limit { get; set; }
}

/// <summary>
/// Information about a stock split
/// </summary>
/// <param name="Ticker">The ticker symbol of the stock split.</param>
/// <param name="ExecutionDate">The execution date of the stock split. On this date the stock split was applied.</param>
/// <param name="SplitFrom">The second number in the split ratio. For example: In a 2-for-1 split, <paramref
/// name="SplitFrom"/> would be 1.</param>
/// <param name="SplitTo">The first number in the split ratio. For example: In a 2-for-1 split, <paramref
/// name="SplitTo"/> would be 2.</param>
public record StockSplit(
	string Ticker,
	[property: JsonPropertyName("execution_date")]
	DateOnly ExecutionDate,
	[property: JsonPropertyName("split_from")]
	decimal SplitFrom,
	[property: JsonPropertyName("split_to")]
	decimal SplitTo);
