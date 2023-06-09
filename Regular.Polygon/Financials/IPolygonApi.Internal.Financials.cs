using Refit;
using Regular.Polygon.Financials;

namespace Regular.Polygon;

internal partial class PolygonApi
{
	public Task<PolygonResponse<IReadOnlyList<FinancialsFiling>>> GetStockFinancials([Query] FinancialsRequest request, CancellationToken cancellationToken = default) =>
		_refitApi.GetStockFinancials(request, cancellationToken);
}

internal partial interface IPolygonApiRefit
{
	[Get("/vX/reference/financials")]
	Task<PolygonResponse<IReadOnlyList<FinancialsFiling>>> GetStockFinancials([Query] FinancialsRequest request, CancellationToken cancellationToken = default);
}
