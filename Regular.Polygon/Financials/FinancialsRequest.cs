namespace Regular.Polygon.Financials;

/// <summary>
/// Request class to hold parameters for the Stock Financials API
/// </summary>
public class FinancialsRequest
{
	/// <summary>
	/// Query by company ticker.
	/// </summary>
	[AliasAs("ticker")]
	public string? Ticker { get; set; }

	/// <summary>
	/// Query by central index key (<a href="https://www.sec.gov/edgar/searchedgar/cik.htm">CIK</a>) Number
	/// </summary>
	[AliasAs("cik")]
	public string? Cik { get; set; }

	/// <summary>
	/// Query by company name.
	/// </summary>
	[AliasAs("company_name")]
	public string? CompanyName { get; set; }

	/// <summary>
	/// Query by standard industrial classification (<a href="https://www.sec.gov/corpfin/division-of-corporation-finance-standard-industrial-classification-sic-code-list">SIC</a>)
	/// </summary>
	[AliasAs("sic")]
	public string? Sic { get; set; }

	/// <summary>
	/// Query by the date when the filing with financials data was filed.T
	/// </summary>
	[AliasAs("filing_date")]
	public DateOnly? FilingDate { get; set; }

	/// <summary>
	/// Query by the date when the filing with financials data was filed.
	/// </summary>
	[AliasAs("filing_date.lte")]
	public DateOnly? FilingDateBefore { get; set; }

	/// <summary>
	/// Query by the date when the filing with financials data was filed.
	/// </summary>
	[AliasAs("filing_date.gte")]
	public DateOnly? FilingDateAfter { get; set; }

	/// <summary>
	/// The period of report for the filing with financials data.
	/// </summary>
	[AliasAs("period_of_report_date")]
	public DateOnly? ReportDate { get; set; }

	/// <summary>
	/// The period of report for the filing with financials data.
	/// </summary>
	[AliasAs("period_of_report_date.lte")]
	public DateOnly? ReportDateBefore { get; set; }

	/// <summary>
	/// The period of report for the filing with financials data.
	/// </summary>
	[AliasAs("period_of_report_date.gte")]
	public DateOnly? ReportDateAfter { get; set; }

	/// <summary>
	/// Query by timeframe. Annual financials originate from 10-K filings, and quarterly financials originate from 10-Q filings.
	/// </summary>
	/// <remarks>
	/// Note: Most companies do not file quarterly reports for Q4 and instead include those financials in their annual report, so some companies my not return quarterly financials for Q4
	/// </remarks>
	[AliasAs("timeframe")]
	public Timeframe? Timeframe { get; set; }

	/// <summary>
	/// Whether or not to include the <c>xpath</c> and <c>formula</c> attributes for each financial data point.
	/// </summary>
	[AliasAs("include_sources")]
	public bool? IncludeSources { get; set; }

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
