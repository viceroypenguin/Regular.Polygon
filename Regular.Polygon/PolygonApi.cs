using System.Diagnostics.CodeAnalysis;
using System.Web;
using CommunityToolkit.Diagnostics;
using Refit;

namespace Regular.Polygon;

/// <summary>
/// A client class that provides methods for calling the polygon.io APIs
/// </summary>
/// <remarks>
/// This class is designed to be short-lived and transient, as a unit-of-work class.
/// </remarks>
public sealed partial class PolygonApi : IAsyncDisposable
{
	private readonly IPolygonApiRefit _refitApi;
	private readonly string _apiKey;
	private readonly PolygonDataStatus _dataStatus;

	/// <summary>
	/// Create a new <see cref="PolygonApi"/> with the specified API key.
	/// </summary>
	/// <param name="apiKey">The API key provided by Polygon.io for access to their APIs.</param>
	/// <param name="dataStatus">A value indicating whether to query live data from the live or delayed Polygon websocket server.</param>
	/// <remarks>
	/// This constructor should only be used by short-lived console applications.
	/// </remarks>
	[SuppressMessage("Design", "CA2000:Dispose objects before losing scope", Justification = "HttpClient won't be disposed.")]
	public PolygonApi(
		string apiKey,
		PolygonDataStatus dataStatus = PolygonDataStatus.None)
	{
		Guard.IsNotNullOrWhiteSpace(apiKey);

		_apiKey = apiKey;
		_dataStatus = dataStatus;

		_refitApi = RestService.For<IPolygonApiRefit>(
			new HttpClient(
				new PolygonMessageHandler(_apiKey)
				{
					InnerHandler =
						new HttpClientHandler
						{
							AutomaticDecompression = System.Net.DecompressionMethods.All,
						},
				})
			{
				BaseAddress = new Uri("https://api.polygon.io/"),
			},
			settings: new()
			{
				ContentSerializer = new SystemTextJsonContentSerializer(DefaultSerializerOptions),
				UrlParameterFormatter = new UrlParameterFormatter(),
			});
	}

	internal PolygonApi(
		IPolygonApiRefit refitApi,
		IOptions<PolygonOptions> options)
	{
		_refitApi = refitApi;

		_apiKey = options.Value.ApiKey;
		_dataStatus = options.Value.DataStatus;
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

	/// <summary>
	/// Release any polygon resources owned by this object.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous dispose operation.
	/// </returns>
	public async ValueTask DisposeAsync()
	{
		if (_stockSocketManager != null)
			await _stockSocketManager.DisposeAsync().ConfigureAwait(false);
	}

	/// <summary>
	/// The default serializer options for deserializing the JSON from polygon.io.
	/// </summary>
	internal static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters =
		{
			new EnumConverterFactory(),
			new UnixMillisecondDateTimeOffsetConverter(),
		},
	};
}

internal partial interface IPolygonApiRefit;
