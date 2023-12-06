using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Regular.Polygon.Financials;

/// <summary>
/// Information about a Financial Statement filed with the SEC
/// </summary>
/// <param name="Cik">The CIK number for the company.</param>
/// <param name="CompanyName">The company name.</param>
/// <param name="Id">The ID of the report as recorded by polygon.io.</param>
/// <param name="FiscalPeriod">Fiscal period of the report according to the company (Q1, Q2, Q3, Q4, or FY).</param>
/// <param name="Timeframe">The timeframe of the report (quarterly, annual or ttm).</param>
/// <param name="Financials">The financial reports contained in the filing.</param>
[ExcludeFromCodeCoverage]
public sealed record FinancialsFiling(
	string Cik,
	[property: JsonPropertyName("company_name")]
	string CompanyName,
	string Id,
	[property: JsonPropertyName("fiscal_period")]
	string FiscalPeriod,
	Timeframe Timeframe,
	[property: JsonPropertyName("financials")]
	FinancialReports Financials)
{
	/// <summary>
	/// The datetime (EST timezone) the filing was accepted by EDGAR
	/// </summary>
	[JsonPropertyName("acceptance_datetime")]
	public string? AcceptanceDateTime { get; set; }

	/// <summary>
	/// The start date of the period that these financials cover
	/// </summary>
	[JsonPropertyName("start_date")]
	public DateOnly? StartDate { get; set; }

	/// <summary>
	/// The end date of the period that these financials cover
	/// </summary>
	[JsonPropertyName("end_date")]
	public DateOnly? EndDate { get; set; }

	/// <summary>
	/// The date that the SEC filing which these financials were derived from was made available. 
	/// </summary>
	/// <remarks>
	/// Note that this is not necessarily the date when this information became public, as some companies may publish a
	/// press release before filing with the SEC.
	/// </remarks>
	[JsonPropertyName("filing_date")]
	public DateOnly? FilingDate { get; set; }

	/// <summary>
	/// Fiscal year of the report according to the company.
	/// </summary>
	[JsonPropertyName("fiscal_year")]
	public string? FiscalYear { get; set; }

	/// <summary>
	/// The URL of the SEC filing that these financials were derived from.
	/// </summary>
	[JsonPropertyName("source_filing_url")]
	public string? SourceFilingUrl { get; set; }

	/// <summary>
	/// The URL of the specific XBRL instance document within the SEC filing that these financials were derived from.
	/// </summary>
	[JsonPropertyName("source_filing_file_url")]
	public string? SourceFilingFileUrl { get; set; }

	/// <summary>
	/// The list of ticker symbols for the company.
	/// </summary>
	public IReadOnlyList<string>? Tickers { get; set; }
}

/// <summary>
/// The financial reports contained in a filing
/// </summary>
[ExcludeFromCodeCoverage]
public sealed record FinancialReports
{
	/// <summary>
	/// Balance sheet
	/// </summary>
	/// <remarks>
	/// Note that the keys in this object can be any of the balance sheet concepts defined in <a
	/// href="http://www.xbrlsite.com/2016/fac/v3/Documentation/FundamentalAccountingConceptsList.html">this table of
	/// fundamental accounting concepts</a> but converted to snake_case.
	/// </remarks>
	[JsonPropertyName("balance_sheet")]
	public IReadOnlyDictionary<string, FinancialReportDataPoint>? BalanceSheet { get; set; }

	/// <summary>
	/// Cash Flow Statement
	/// </summary>
	/// <remarks>
	/// Note that the keys in this object can be any of the balance sheet concepts defined in <a
	/// href="http://www.xbrlsite.com/2016/fac/v3/Documentation/FundamentalAccountingConceptsList.html">this table of
	/// fundamental accounting concepts</a> but converted to snake_case.
	/// </remarks>
	[JsonPropertyName("cash_flow_statement")]
	public IReadOnlyDictionary<string, FinancialReportDataPoint>? CashFlowStatement { get; set; }

	/// <summary>
	/// Income Statement
	/// </summary>
	/// <remarks>
	/// Note that the keys in this object can be any of the balance sheet concepts defined in <a
	/// href="http://www.xbrlsite.com/2016/fac/v3/Documentation/FundamentalAccountingConceptsList.html">this table of
	/// fundamental accounting concepts</a> but converted to snake_case.
	/// </remarks>
	[JsonPropertyName("income_statement")]
	public IReadOnlyDictionary<string, FinancialReportDataPoint>? IncomeStatement { get; set; }

	/// <summary>
	/// Comprehensive Income
	/// </summary>
	/// <remarks>
	/// Note that the keys in this object can be any of the balance sheet concepts defined in <a
	/// href="http://www.xbrlsite.com/2016/fac/v3/Documentation/FundamentalAccountingConceptsList.html">this table of
	/// fundamental accounting concepts</a> but converted to snake_case.
	/// </remarks>
	[JsonPropertyName("comprehensive_income")]
	public IReadOnlyDictionary<string, FinancialReportDataPoint>? ComprehensiveIncome { get; set; }
}

/// <summary>
/// The source where this data point came from. 
/// </summary>
public enum FinancialReportDataPointSource
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "direct_report")] SourceDirectReport,
	[EnumMember(Value = "intra_report_impute")] SourceIntraReportImpute,
	[EnumMember(Value = "inter_report_derive")] SourceInterReportDerived
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// An individual financial data point
/// </summary>
/// <param name="Label">A human readable label for the financial data point.</param>
/// <param name="Unit">The unit of the financial data point.</param>
/// <param name="Value">The value of the financial data point.</param>
/// <param name="Order">An indicator of what order within the statement that you would find this data point.</param>
[ExcludeFromCodeCoverage]
public sealed record FinancialReportDataPoint(
	string Label,
	string Unit,
	decimal Value,
	int Order)
{
	/// <summary>
	/// The source where this data point came from. 
	/// </summary>
	public FinancialReportDataPointSource? Source { get; set; }

	/// <summary>
	/// The <a href="https://en.wikipedia.org/wiki/XPath">XPath 1.0</a> query that identifies the fact from within the
	/// XBRL source file. This value is only returned for data points taken directly from XBRL when the <see
	/// cref="FinancialsRequest.IncludeSources"/> query parameter is <see langword="true"/> and if <see cref="Source"/>
	/// is <see cref="FinancialReportDataPointSource.SourceDirectReport"/>.
	/// </summary>
	[JsonPropertyName("xpath")]
	public string? XPath { get; set; }

	/// <summary>
	/// The name of the formula used to derive this data point from other financial data points. Information about the
	/// formulas can be found <a
	/// href="http://xbrlsite.azurewebsites.net/2020/reporting-scheme/us-gaap/fac/documentation/ImputeRulesList.html">here</a>.
	/// This value is only returned for data points that are not explicitly expressed within the XBRL source file when
	/// the <see cref="FinancialsRequest.IncludeSources"/> query parameter is <see langword="true"/> and if <see
	/// cref="Source"/> is <see cref="FinancialReportDataPointSource.SourceIntraReportImpute"/>.
	/// </summary>
	public string? Formula { get; set; }

	/// <summary>
	/// The list of report IDs (or errata) which were used to derive this data point. This value is only returned for
	/// data points taken directly from XBRL when the <see cref="FinancialsRequest.IncludeSources"/> query parameter is
	/// <see langword="true"/> and if <see cref="Source"/> is <see
	/// cref="FinancialReportDataPointSource.SourceInterReportDerived"/>.
	/// </summary>
	[JsonPropertyName("derived_from")]
	public IReadOnlyList<string>? DerivedFrom { get; set; }
}
