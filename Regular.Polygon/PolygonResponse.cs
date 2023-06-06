using System.Text.Json.Serialization;

namespace Regular.Polygon;

/// <summary>
/// A response object containing request and status information.
/// </summary>
/// <typeparam name="T">The type of the returned data from the API call.</typeparam>
public record PolygonResponse<T>
{
	/// <summary>
	/// A request id assigned by the server.
	/// </summary>
	[JsonPropertyName("request_id")]
	public required string RequestId { get; set; }

	/// <summary>
	/// The status of this request's response.
	/// </summary>
	public required string Status { get; set; }

	/// <summary>
	/// If present, the number of records in the response.
	/// </summary>
	public int? Count { get; set; }

	/// <summary>
	/// The results of the API call.
	/// </summary>
	public T? Results { get; set; }

	/// <summary>
	/// If present, this value can be used to fetch the next page of data.
	/// </summary>
	[JsonPropertyName("next_url")]
	public string? NextUrl { get; set; }
}
