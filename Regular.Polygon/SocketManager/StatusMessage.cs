using System.Text.Json.Serialization;

namespace Regular.Polygon.SocketManager;

internal sealed record StatusMessage(
	[property: JsonPropertyName("ev")]
	string EventType,
	[property: JsonPropertyName("status")]
	string Status,
	[property: JsonPropertyName("message")]
	string Message);

