namespace Regular.Polygon;

public partial interface IPolygonApi
{
	public static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
	};
}
