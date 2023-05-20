using System.Text.Json.Serialization;
using CommunityToolkit.Diagnostics;
using System.Web;

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
			new JsonStringEnumConverter(),
		},
	};

	private static async Task<IReadOnlyList<T>> GetFullList<T>(
		Task<PolygonResponse<IReadOnlyList<T>>> responseTask,
		Func<string, Task<PolygonResponse<IReadOnlyList<T>>>> nextUrlFunc)
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

			response = await nextUrlFunc(cursor).ConfigureAwait(false);
		}

		return list.ToList();
	}
}
