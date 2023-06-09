﻿using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Regular.Polygon;

/// <summary>
/// Holding class for extension methods.
/// </summary>
public static class PolygonServiceCollectionExtensions
{
	/// <summary>
	/// Registers the <see cref="IPolygonApi"/> infrastructure.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
	/// <param name="apiKey">A polygon.io API Key</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained</returns>
	public static IServiceCollection AddPolygonApi(
		this IServiceCollection services,
		string apiKey)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNullOrWhiteSpace(apiKey);

		services.Configure<PolygonOptions>(o => o.ApiKey = apiKey);
		services.AddTransient<PolygonMessageHandler>();
		services
			.AddRefitClient<IPolygonApiRefit>(settings: new()
			{
				ContentSerializer = new SystemTextJsonContentSerializer(IPolygonApi.DefaultSerializerOptions),
				UrlParameterFormatter = new UrlParameterFormatter(),
			})
			.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.polygon.io/"))
			.ConfigurePrimaryHttpMessageHandler(() =>
				new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All, })
			.AddHttpMessageHandler<PolygonMessageHandler>();
		services.AddSingleton<IPolygonApi, PolygonApi>();

		return services;
	}
}
