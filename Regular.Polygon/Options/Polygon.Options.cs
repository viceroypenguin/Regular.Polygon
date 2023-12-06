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
}
