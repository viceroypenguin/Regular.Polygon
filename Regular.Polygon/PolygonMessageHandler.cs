using System.Web;

namespace Regular.Polygon;

internal sealed class PolygonMessageHandler : DelegatingHandler
{
	private readonly string _apiKey;

	public PolygonMessageHandler(IOptions<PolygonOptions> options)
	{
		_apiKey = options.Value.ApiKey;
	}

	public PolygonMessageHandler(string apiKey)
	{
		_apiKey = apiKey;
	}

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var uriBuilder = new UriBuilder(request.RequestUri!);
		var paramValues = HttpUtility.ParseQueryString(uriBuilder.Query);

		if (string.IsNullOrWhiteSpace(paramValues.Get("apiKey")))
		{
			paramValues.Add("apiKey", _apiKey);

			uriBuilder.Query = paramValues.ToString();
			request.RequestUri = uriBuilder.Uri;
		}

		return base.SendAsync(request, cancellationToken);
	}
}
