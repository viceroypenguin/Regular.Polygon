using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Regular.Polygon.Ticker;

/// <summary>
/// Detailed information about a Ticker
/// </summary>
/// <param name="Ticker">The exchange symbol that this item is traded under.</param>
/// <param name="Name">The exchange symbol that this item is traded under.</param>
/// <param name="Market">The market type of the asset.</param>
/// <param name="Locale">The locale of the asset.</param>
/// <param name="Active">Whether or not the asset is actively traded. False means the asset has been delisted.</param>
[ExcludeFromCodeCoverage]
public record TickerDetail(
	string Ticker,
	string Name,
	AssetClass Market,
	Locale Locale,
	bool Active)
{
	/// <summary>
	/// The company's headquarters address
	/// </summary>
	[JsonPropertyName("address")]
	public AddressObject? Address { get; init; }

	/// <summary>
	/// Company branding
	/// </summary>
	[JsonPropertyName("branding")]
	public BrandingObject? Branding { get; init; }

	/// <summary>
	/// The CIK number for this ticker. Find more information <a href="https://en.wikipedia.org/wiki/Central_Index_Key">here</a>.
	/// </summary>
	[JsonPropertyName("cik")]
	public string? Cik { get; init; }

	/// <summary>
	/// The composite OpenFIGI number for this ticker. Find more information <a href="https://www.openfigi.com/assets/content/Open_Symbology_Fields-2a61f8aa4d.pdf">here</a>.
	/// </summary>
	[JsonPropertyName("composite_figi")]
	public string? CompositeFigi { get; init; }

	/// <summary>
	/// The ISO code of the primary listing exchange for this asset.
	/// </summary>
	[JsonPropertyName("currency_name")]
	public string? CurrencyName { get; init; }

	/// <summary>
	/// A description of the company and what they do/offer.
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; init; }

	/// <summary>
	/// The URL of the company's website homepage.
	/// </summary>
	[JsonPropertyName("homepage_url")]
	public string? HomepageUrl { get; init; }

	/// <summary>
	/// The date that the symbol was first publicly listed.
	/// </summary>
	[JsonPropertyName("list_date")]
	public DateOnly? ListDate { get; init; }

	/// <summary>
	/// The most recent close price of the ticker multiplied by weighted outstanding shares.
	/// </summary>
	[JsonPropertyName("market_cap")]
	public decimal? MarketCap { get; init; }

	/// <summary>
	/// The phone number for the company behind this ticker.
	/// </summary>
	[JsonPropertyName("phone_number")]
	public string? PhoneNumber { get; init; }

	/// <summary>
	/// The ISO code of the primary listing exchange for this asset.
	/// </summary>
	[JsonPropertyName("primary_exchange")]
	public string? PrimaryExchange { get; init; }

	/// <summary>
	/// Round lot size of this security.
	/// </summary>
	[JsonPropertyName("round_lot")]
	public int? RoundLot { get; init; }

	/// <summary>
	/// The share Class OpenFIGI number for this ticker. Find more information <a href="https://www.openfigi.com/assets/content/Open_Symbology_Fields-2a61f8aa4d.pdf">here</a>.
	/// </summary>
	[JsonPropertyName("share_class_figi")]
	public string? ShareClassFigi { get; init; }

	/// <summary>
	/// The recorded number of outstanding shares for this particular share class.
	/// </summary>
	[JsonPropertyName("share_class_shares_outstanding")]
	public long? ShareClassSharesOutstanding { get; init; }

	/// <summary>
	/// The standard industrial classification code for this ticker. For a list of SIC Codes, see the SEC's <a href="https://www.sec.gov/info/edgar/siccodes.htm">SIC Code List</a>.
	/// </summary>
	[JsonPropertyName("sic_code")]
	public string? SicCode { get; init; }

	/// <summary>
	/// A description of this ticker's SIC code.
	/// </summary>
	[JsonPropertyName("sic_description")]
	public string? SicDescription { get; init; }

	/// <summary>
	/// The root of a specified ticker. 
	/// </summary>
	[JsonPropertyName("ticker_root")]
	public string? TickerRoot { get; init; }

	/// <summary>
	/// The approximate number of employees for the company.
	/// </summary>
	[JsonPropertyName("total_employees")]
	public int? TotalEmployees { get; init; }

	/// <summary>
	/// The shares outstanding calculated assuming all shares of other share classes are converted to this share class.
	/// </summary>
	[JsonPropertyName("weighted_shares_outstanding")]
	public long WeightedSharesOutstanding { get; init; }

	/// <summary>
	/// The company's headquarters address
	/// </summary>
	/// <param name="Address1">The first line of the company's headquarters address.</param>
	/// <param name="City">The city of the company's headquarters address.</param>
	/// <param name="PostalCode">The postal code of the company's headquarters address.</param>
	/// <param name="State">The state of the company's headquarters address.</param>
	public record AddressObject(
		[property: JsonPropertyName("address1")]
		string Address1,
		[property: JsonPropertyName("city")]
		string City,
		[property: JsonPropertyName("postal_code")]
		string PostalCode,
		[property: JsonPropertyName("state")]
		string State);

	/// <summary>
	/// Company branding
	/// </summary>
	/// <param name="IconUrl">A link to this ticker's company's icon.</param>
	/// <param name="LogoUrl">A link to this ticker's company's logo.</param>
	public record BrandingObject(
		[property: JsonPropertyName("icon_url")]
		string IconUrl,
		[property: JsonPropertyName("logo_url")]
		string LogoUrl);
}
