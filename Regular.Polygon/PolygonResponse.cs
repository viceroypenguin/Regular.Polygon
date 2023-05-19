using System.Text.Json.Serialization;

namespace Regular.Polygon;

public record PolygonResponse<T>(
	[property: JsonPropertyName("request_id")]
	string RequestId,
	string Status,
	T? Results);
