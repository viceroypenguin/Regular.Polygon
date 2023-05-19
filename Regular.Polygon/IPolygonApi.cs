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
	};
}
