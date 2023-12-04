using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Regular.Polygon.Tests;

public class PolygonFixture
{
	private readonly ServiceProvider _serviceProvider;

	public PolygonApi Client { get; }

	public PolygonFixture()
	{
		var configuration = new ConfigurationBuilder()
			.AddJsonFile("secrets.json", optional: true)
			.AddEnvironmentVariables()
			.Build();

		var services = new ServiceCollection();

		var apiKey = configuration["ApiKey"];
		Guard.IsNotNullOrWhiteSpace(apiKey);
		services.AddPolygonApi(o => o.ApiKey = apiKey);

		_serviceProvider = services.BuildServiceProvider();
		Client = _serviceProvider.GetRequiredService<PolygonApi>();
	}
}
