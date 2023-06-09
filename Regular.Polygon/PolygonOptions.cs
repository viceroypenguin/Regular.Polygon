using System.ComponentModel.DataAnnotations;

namespace Regular.Polygon;

/// <summary>
/// The value of this enum is used to determine which Polygon server to use for web-socket data.
/// </summary>
public enum PolygonDataStatus
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

	/// <summary>
	/// The websocket connection will be made to <c>delayed.polygon.io</c>, which delivers data on a 15 minute delay.
	/// </summary>
	Delayed,

	/// <summary>
	/// The websocket connection will be made to <c>socket.polygon.io</c>, which delivers data live.
	/// </summary>
	Live,
}

/// <summary>
/// Implements the IOptions pattern for configuring <c>Regular.Polygon</c>.
/// </summary>
public class PolygonOptions
{
	/// <summary>
	/// The API key provided by polygon.io for access to their APIs.
	/// </summary>
	/// <remarks>
	/// You can obtain an API key by visiting <see href="https://polygon.io/dashboard/api-keys"/>.
	/// </remarks>
	[Required]
	public required string ApiKey { get; set; }

	/// <summary>
	/// This value is used to determine which Polygon server to use for web-socket data.
	/// </summary>
	public PolygonDataStatus DataStatus { get; set; }
}
