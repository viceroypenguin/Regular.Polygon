using System.ComponentModel.DataAnnotations;

namespace Regular.Polygon;

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
}
