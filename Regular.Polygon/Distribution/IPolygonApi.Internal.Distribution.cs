﻿using Refit;
using Regular.Polygon.Distribution;

namespace Regular.Polygon;

internal partial interface IPolygonApiRefit
{
	[Get("/v3/reference/splits")]
	Task<PolygonResponse<IReadOnlyList<StockSplit>>> GetStockSplits([Query] StockSplitRequest? request = null, CancellationToken cancellationToken = default);

	[Get("/v3/reference/dividends")]
	Task<PolygonResponse<IReadOnlyList<Dividend>>> GetDividends([Query] DividendRequest? request = null, CancellationToken cancellationToken = default);
}
