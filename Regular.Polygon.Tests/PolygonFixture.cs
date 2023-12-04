using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Regular.Polygon.Tests;

public static class PolygonFixture
{
	private static ServiceProvider? s_serviceProvider;

	public static IReadOnlyList<object[]> Data { get; } = GetData();

	private static IReadOnlyList<object[]> GetData()
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("secrets.json", optional: true)
			.AddEnvironmentVariables()
			.Build();
		var apiKey = configuration["ApiKey"];
		Guard.IsNotNullOrWhiteSpace(apiKey);

		s_serviceProvider = new ServiceCollection()
			.AddPolygonApi(o => o.ApiKey = apiKey)
			.BuildServiceProvider();

		return
		[
			[s_serviceProvider.GetRequiredService<PolygonApi>()],
			[new PolygonApi(apiKey)],
		];
	}
}
