using System.Text.Json.Serialization;

namespace Regular.Polygon.Ticker;

/// <summary>
/// Request class to hold parameters for the Ticker Search api
/// </summary>
public class TickerSearchRequest
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
	/// Specify the type of the tickers. Find the supported types via <see cref="IPolygonApi.GetTickerTypes(TickerTypeRequest?)"/>
	/// </summary>
	[AliasAs("type")]
	public string? TickerType { get; set; }

	/// <summary>
	/// Filter by market type. 
	/// </summary>
	[AliasAs("market")]
	public AssetClass? Market { get; set; }

	/// <summary>
	/// Specify the primary exchange of the asset in the ISO code format. Find more information about the ISO codes <a
	/// href="https://www.iso20022.org/market-identifier-codes">at the ISO org website</a>. 
	/// </summary>
	[AliasAs("exchange")]
	public string? Exchange { get; set; }

	/// <summary>
	/// Specify the CUSIP code of the asset you want to search for. Find more information about CUSIP codes <a
	/// href="https://www.cusip.com/identifiers.html#/CUSIP">at their website</a>.
	/// </summary>
	/// <remarks>
	/// Note: Although you can query by CUSIP, due to legal reasons we do not return the CUSIP in the response.
	/// </remarks>
	[AliasAs("cusip")]
	public string? Cusip { get; set; }

	/// <summary>
	/// Specify the CIK of the asset you want to search for. Find more information about CIK codes <a href="https://www.sec.gov/edgar/searchedgar/cik.htm">at their website</a>.
	/// </summary>
	[AliasAs("cik")]
	public string? Cik { get; set; }

	/// <summary>
	/// Specify a point in time to retrieve tickers available on that date. 
	/// </summary>
	[AliasAs("date")]
	public DateOnly? Date { get; set; }

	/// <summary>
	/// Search for terms within the ticker and/or company name.
	/// </summary>
	[AliasAs("search")]
	public string? Search { get; set; }

	/// <summary>
	/// Specify if the tickers returned should be actively traded on the queried date. 
	/// </summary>
	[AliasAs("active")]
	public bool Active { get; set; } = true;

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
/// A ticker search result record
/// </summary>
/// <param name="Ticker">The exchange symbol that this item is traded under.</param>
/// <param name="Name">The exchange symbol that this item is traded under.</param>
/// <param name="Market">The market type of the asset.</param>
/// <param name="Locale">The locale of the asset.</param>
public record TickerSearchResult(
	string Ticker,
	string Name,
	AssetClass Market,
	Locale Locale)
{
	/// <summary>
	/// The type of the asset.
	/// </summary>
	public string? Type { get; init; }

	/// <summary>
	/// Whether or not the asset is actively traded. False means the asset has been delisted.
	/// </summary>
	public bool? Active { get; init; }

	/// <summary>
	/// The CIK number for this ticker. Find more information <a href="https://en.wikipedia.org/wiki/Central_Index_Key">here</a>.
	/// </summary>
	public string? Cik { get; init; }

	/// <summary>
	/// The ISO code of the primary listing exchange for this asset.
	/// </summary>
	[JsonPropertyName("primary_exchange")]
	public string? PrimaryExchange { get; init; }

	/// <summary>
	/// The ISO code of the primary listing exchange for this asset.
	/// </summary>
	[JsonPropertyName("currency_name")]
	public string? CurrencyName { get; init; }

	/// <summary>
	/// The last date that the asset was traded.
	/// </summary>
	[JsonPropertyName("delisted_utc")]
	public DateTimeOffset? Delisted { get; init; }

	/// <summary>
	/// The information is accurate up to this time.
	/// </summary>
	[JsonPropertyName("last_updated_utc")]
	public DateTimeOffset? LastUpdated { get; init; }

	/// <summary>
	/// The share Class OpenFIGI number for this ticker. Find more information <a href="https://www.openfigi.com/assets/content/Open_Symbology_Fields-2a61f8aa4d.pdf">here</a>.
	/// </summary>
	[JsonPropertyName("share_class_figi")]
	public string? ShareClassFigi { get; init; }

	/// <summary>
	/// The composite OpenFIGI number for this ticker. Find more information <a href="https://www.openfigi.com/assets/content/Open_Symbology_Fields-2a61f8aa4d.pdf">here</a>.
	/// </summary>
	[JsonPropertyName("composite_figi")]
	public string? CompositeFigi { get; init; }
}
