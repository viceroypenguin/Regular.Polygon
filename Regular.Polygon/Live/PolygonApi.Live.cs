using Regular.Polygon.Live;
using Regular.Polygon.SocketManager;

namespace Regular.Polygon;

public partial class PolygonApi
{
	private PolygonSocketManager? _stockSocketManager;

	/// <summary>
	/// Stream real-time minute aggregates for a given stock ticker symbol.
	/// </summary>
	/// <param name="symbol">The stock ticker symbol for which to get live aggrigates.</param>
	/// <returns>An async stream of minute aggregates for the stock ticker symbol.</returns>
	public IAsyncEnumerable<LiveAggregateBar> GetStockLiveAggregatesByMinute(string symbol)
	{
		if (_stockSocketManager == null)
			// don't care about original value, only care that _a_ `PolygonSocketManager` got set.
			_ = Interlocked.CompareExchange(ref _stockSocketManager, new(_dataStatus, "stocks", _apiKey), null);

		return _stockSocketManager.GetEventsForKey<LiveAggregateBar>($"AM.{symbol}", 4);
	}

	/// <summary>
	/// Stream real-time second aggregates for a given stock ticker symbol.
	/// </summary>
	/// <param name="symbol">The stock ticker symbol for which to get live aggrigates.</param>
	/// <returns>An async stream of second aggregates for the stock ticker symbol.</returns>
	public IAsyncEnumerable<LiveAggregateBar> GetStockLiveAggregatesBySecond(string symbol)
	{
		if (_stockSocketManager == null)
			// don't care about original value, only care that _a_ `PolygonSocketManager` got set.
			_ = Interlocked.CompareExchange(ref _stockSocketManager, new(_dataStatus, "stocks", _apiKey), null);

		return _stockSocketManager.GetEventsForKey<LiveAggregateBar>($"A.{symbol}", 4);
	}
}
