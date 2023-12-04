using Refit;
using Regular.Polygon.Financials;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{
	[Get("/vX/reference/financials")]
	Task<PolygonResponse<IReadOnlyList<FinancialsFiling>>> GetStockFinancials([Query] FinancialsRequest request, CancellationToken cancellationToken = default);
}
