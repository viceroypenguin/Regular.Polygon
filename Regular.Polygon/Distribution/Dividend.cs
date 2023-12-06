using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Refit;

namespace Regular.Polygon.Distribution;

/// <summary>
/// The type of dividend.
/// </summary>
public enum DividendType
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// Dividends that have been paid and/or are expected to be paid on consistent schedules.
	/// </summary>
	[EnumMember(Value = "CD")] ConsistentDividend,

	/// <summary>
	/// Special Cash dividends that have been paid that are infrequent or unusual, and/or can not be expected to occur
	/// in the future.
	/// </summary>
	[EnumMember(Value = "SC")] SpecialCash,

	/// <summary>
	/// Long-Term Capital Gains Distribution
	/// </summary>
	[EnumMember(Value = "LT")] LongTermCapitalGains,

	/// <summary>
	/// Short-Term Capital Gains Distribution
	/// </summary>
	[EnumMember(Value = "ST")] ShortTermCapitalGains,
}

/// <summary>
/// Request class to hold parameters for the Dividend api
/// </summary>
[ExcludeFromCodeCoverage]
public class DividendRequest
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
	/// Query for dividends with this ex-dividend date
	/// </summary>
	[AliasAs("ex_dividend_date")]
	public DateOnly? ExDividendDate { get; set; }

	/// <summary>
	/// Query for dividends with a ex-dividend date before this date
	/// </summary>
	[AliasAs("ex_dividend_date.lte")]
	public DateOnly? ExDividendBefore { get; set; }

	/// <summary>
	/// Query for dividends with a ex-dividend date after this date
	/// </summary>
	[AliasAs("ex_dividend_date.gte")]
	public DateOnly? ExDividendAfter { get; set; }

	/// <summary>
	/// Query for dividends with this pay date
	/// </summary>
	[AliasAs("pay_date")]
	public DateOnly? PayDate { get; set; }

	/// <summary>
	/// Query for dividends with a pay date before this date
	/// </summary>
	[AliasAs("pay_date.lte")]
	public DateOnly? PayBefore { get; set; }

	/// <summary>
	/// Query for dividends with a pay date after this date
	/// </summary>
	[AliasAs("pay_date.gte")]
	public DateOnly? PayAfter { get; set; }

	/// <summary>
	/// Query for dividends with this declaration date
	/// </summary>
	[AliasAs("declaration_date")]
	public DateOnly? DeclarationDate { get; set; }

	/// <summary>
	/// Query for dividends with a declaration date before this date
	/// </summary>
	[AliasAs("declaration_date.lte")]
	public DateOnly? DeclarationBefore { get; set; }

	/// <summary>
	/// Query for dividends with a declaration date after this date
	/// </summary>
	[AliasAs("declaration_date.gte")]
	public DateOnly? DeclarationAfter { get; set; }

	/// <summary>
	/// The number of times per year the dividend is paid out. Possible values are 0 (one-time), 1
	/// (annually), 2 (bi-annually), 4 (quarterly), and 12 (monthly).
	/// </summary>
	[AliasAs("frequency")]
	public int? Frequency { get; set; }

	/// <summary>
	/// Query for dividends with this cash amount
	/// </summary>
	[AliasAs("cash_amount")]
	public decimal? CashAmountDate { get; set; }

	/// <summary>
	/// Query for dividends with a cash amount less than this amount
	/// </summary>
	[AliasAs("cash_amount.lte")]
	public decimal? CashAmountBefore { get; set; }

	/// <summary>
	/// Query for dividends with a cash amount more than this amount
	/// </summary>
	[AliasAs("cash_amount.gte")]
	public decimal? CashAmountAfter { get; set; }

	/// <summary>
	/// Query by the type of dividend.
	/// </summary>
	[AliasAs("dividend_type")]
	public DividendType? DividendType { get; set; }

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
/// Information about a cash dividend
/// </summary>
/// <param name="Ticker">The ticker symbol of the dividend.</param>
/// <param name="DividendType">The type of dividend.</param>
/// <param name="CashAmount">The cash amount of the dividend per share owned.</param>
/// <param name="ExDividendDate">The date that the stock first trades without the dividend, determined by the
/// exchange.</param>
/// <param name="Frequency">The number of times per year the dividend is paid out. Possible values are 0 (one-time), 1
/// (annually), 2 (bi-annually), 4 (quarterly), and 12 (monthly).</param>
[ExcludeFromCodeCoverage]
public sealed record Dividend(
	string Ticker,
	[property: JsonPropertyName("dividend_type")]
	DividendType DividendType,
	[property: JsonPropertyName("cash_amount")]
	decimal CashAmount,
	[property: JsonPropertyName("ex_dividend_date")]
	DateOnly ExDividendDate,
	int Frequency)
{
	/// <summary>
	/// The currency in which the dividend is paid.
	/// </summary>
	public string? Currency { get; set; }

	/// <summary>
	/// The date that the dividend was announced.
	/// </summary>
	[JsonPropertyName("declaration_date")]
	public DateOnly? DeclarationDate { get; set; }

	/// <summary>
	/// The date that the dividend is paid out.
	/// </summary>
	[JsonPropertyName("pay_date")]
	public DateOnly? PayDate { get; set; }

	/// <summary>
	/// The date that the stock must be held to receive the dividend, set by the company.
	/// </summary>
	[JsonPropertyName("record_date")]
	public DateOnly? RecordDate { get; set; }
}
