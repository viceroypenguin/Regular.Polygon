using Regular.Polygon.Options;

namespace Regular.Polygon;

public partial class PolygonApi
{
	/// <summary>
	/// Get an options contract. This response will have detailed information about the options ticker.
	/// </summary>
	/// <param name="ticker">
	/// The symbol of the contract.  You can learn more about the structure of options tickers <a
	/// href="https://polygon.io/blog/how-to-read-a-stock-options-ticker/">here</a>.
	/// </param>
	/// <param name="asOf">
	/// Specify a point in time for the contract as of this date. Defaults to today's date.
	/// </param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>Detailed information on an options contract.</returns>
	public Task<PolygonResponse<OptionsContractDetail>> GetOptionsContractDetail(string ticker, DateOnly? asOf = null, CancellationToken cancellationToken = default) =>
		_refitApi.GetOptionsContractDetail(ticker, asOf, cancellationToken);

	/// <summary>
	/// Query for historial options contracts. This provides both active and expired options contracts.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Options Contracts Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of Options Contracts matching the query.</returns>
	public Task<PolygonResponse<IReadOnlyList<OptionsContractDetail>>> SearchOptionsContracts(OptionsContractSearch? request = null, CancellationToken cancellationToken = default) =>
		_refitApi.SearchOptionsContracts(request, cancellationToken);

	/// <summary>
	/// Query for historial options contracts. This provides both active and expired options contracts.
	/// </summary>
	/// <param name="request">Request object to hold parameters for the Options Contracts Search api.</param>
	/// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
	/// <returns>A list of Options Contracts matching the query.</returns>
	public Task<IReadOnlyList<OptionsContractDetail>> SearchOptionsContractsAll(OptionsContractSearch? request = null, CancellationToken cancellationToken = default)
	{
		return GetFullList(SearchOptionsContracts(request, cancellationToken), SearchTickersCursor, cancellationToken);

		Task<PolygonResponse<IReadOnlyList<OptionsContractDetail>>> SearchTickersCursor(string cursor, CancellationToken cancellationToken = default) =>
			_refitApi.SearchOptionsContractsCursor(cursor, cancellationToken);
	}
}
