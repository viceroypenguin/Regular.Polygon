using System.Diagnostics.CodeAnalysis;
using Refit;

namespace Regular.Polygon.Options;

/// <summary>
/// Request class to hold parameters for the Options Contract Search api
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class OptionsContractSearch
{
	/// <summary>
	/// Specify a ticker symbol. 
	/// </summary>
	[AliasAs("underlying_ticker")]
	public string? UnderlyingTicker { get; set; }

	/// <summary>
	/// Search ticker symbol string comparison greater than.
	/// </summary>
	[AliasAs("underlying_ticker.gte")]
	public string? UnderlyingTickerGreaterThan { get; set; }

	/// <summary>
	/// Search ticker symbol string comparison less than.
	/// </summary>
	[AliasAs("underlying_ticker.lte")]
	public string? UnderlyingTickerLessThan { get; set; }

	/// <summary>
	/// Specify the type of option contract types. Valid options are "call" or "put".
	/// </summary>
	[AliasAs("type")]
	public string? ContractType { get; set; }

	/// <summary>
	/// Query by contract expiration. 
	/// </summary>
	[AliasAs("expiration_date")]
	public DateOnly? ExpirationDate { get; set; }

	/// <summary>
	/// Query by contract expiration greater than.
	/// </summary>
	[AliasAs("expiration_date.gte")]
	public DateOnly? ExpirationDateGreaterThan { get; set; }

	/// <summary>
	/// Query by contract expiration less than.
	/// </summary>
	[AliasAs("expiration_date.lte")]
	public DateOnly? ExpirationDateLessThan { get; set; }

	/// <summary>
	/// Specify a point in time for contracts as of this date. Defaults to today's date.
	/// </summary>
	[AliasAs("as_of")]
	public DateOnly? AsOf { get; set; }

	/// <summary>
	/// Query by strike price of a contract.
	/// </summary>
	[AliasAs("strike_price")]
	public decimal? StrikePrice { get; set; }

	/// <summary>
	/// Query by strike price of a contract greater than.
	/// </summary>
	[AliasAs("strike_price.gte")]
	public decimal? StrikePriceGreaterThan { get; set; }

	/// <summary>
	/// Query by strike price of a contract less than.
	/// </summary>
	[AliasAs("strike_price.lte")]
	public decimal? StrikePriceLessThan { get; set; }

	/// <summary>
	/// Query for expired contracts. Default is false.
	/// </summary>
	[AliasAs("expired")]
	public bool? Expired { get; set; }

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
	/// Limit the number of results returned, default is 10 and max is 1000.
	/// </summary>
	[AliasAs("limit")]
	public int? Limit { get; set; }
}
