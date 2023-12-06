using Refit;
using Regular.Polygon.Options;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{

	[Get("/v3/reference/options/contracts/{ticker}")]
	Task<PolygonResponse<OptionsContractDetail>> GetOptionsContractDetail(string ticker, [Query] DateOnly? date = null, CancellationToken cancellationToken = default);

	[Get("/v3/reference/options/contracts")]
	Task<PolygonResponse<IReadOnlyList<OptionsContractDetail>>> SearchOptionsContracts([Query] OptionsContractSearch? request = null, CancellationToken cancellationToken = default);

	[Get("/v3/reference/options/contracts")]
	Task<PolygonResponse<IReadOnlyList<OptionsContractDetail>>> SearchOptionsContractsCursor([Query] string cursor, CancellationToken cancellationToken = default);
}
