using System.Text.Json.Serialization;

namespace Regular.Polygon;

/// <summary>
/// A response object containing request and status information.
/// </summary>
/// <typeparam name="T">The type of the returned data from the API call.</typeparam>
/// <param name="RequestId">A request id assigned by the server.</param>
/// <param name="Status">The status of this request's response.</param>
/// <param name="Count">If present, the number of records in the response.</param>
/// <param name="Results">The results of the API call.</param>
/// <param name="NextUrl">If present, this value can be used to fetch the next page of data.</param>
public record PolygonResponse<T>(
	[property: JsonPropertyName("request_id")]
	string RequestId,
	string Status,
	int? Count,
	T? Results,
	[property: JsonPropertyName("next_url")]
	string? NextUrl);
