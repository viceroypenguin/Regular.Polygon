using System.Web;
using CommunityToolkit.Diagnostics;

namespace Regular.Polygon;

/// <summary>
/// An interface that provides methods for calling the polygon.io APIs
/// </summary>
public partial interface IPolygonApi
{
	/// <summary>
	/// The default serializer options for deserializing the JSON from polygon.io.
	/// </summary>
	public static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters =
		{
			new EnumConverterFactory(),
			new UnixMillisecondDateTimeOffsetConverter(),
		},
	};
}

internal sealed partial class PolygonApi : IPolygonApi
{
	private readonly IPolygonApiRefit _refitApi;

	public PolygonApi(IPolygonApiRefit refitApi)
	{
		_refitApi = refitApi;
	}

	private static async Task<IReadOnlyList<T>> GetFullList<T>(
		Task<PolygonResponse<IReadOnlyList<T>>> responseTask,
		Func<string, CancellationToken, Task<PolygonResponse<IReadOnlyList<T>>>> nextUrlFunc,
		CancellationToken cancellationToken)
	{
		var response = await responseTask.ConfigureAwait(false);
		if (string.IsNullOrWhiteSpace(response.NextUrl))
			return response.Results!;

		var list = Enumerable.Empty<T>();
		while (!string.IsNullOrWhiteSpace(response.NextUrl))
		{
			list = list.Concat(response.Results!);

			var nextUrl = response.NextUrl;
			var query = HttpUtility.ParseQueryString(new Uri(nextUrl).Query);
			var cursor = query.Get("cursor");
			Guard.IsNotNullOrWhiteSpace(cursor);

			response = await nextUrlFunc(cursor, cancellationToken).ConfigureAwait(false);
		}

		return list.ToList();
	}
}

internal partial interface IPolygonApiRefit
{

}
