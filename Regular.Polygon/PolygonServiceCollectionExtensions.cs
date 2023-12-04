using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Regular.Polygon;

/// <summary>
/// Holding class for extension methods.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PolygonServiceCollectionExtensions
{
	/// <summary>
	/// Registers the <see cref="PolygonApi"/> infrastructure, using the data in the <c>PolygonOptions</c>
	/// section of the system configuration.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained</returns>
	/// <remarks>
	/// <para>Extracts configuration from the section titled <c>PolygonOptions</c></para>
	/// </remarks>
	public static IServiceCollection AddPolygonApi(this IServiceCollection services) =>
		services.AddPolygonApi(nameof(PolygonOptions));

	/// <summary>
	/// Registers the <see cref="PolygonApi"/> infrastructure.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
	/// <param name="sectionPath">The path to the <see cref="IConfiguration"/> section containing polygon options</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained</returns>
	public static IServiceCollection AddPolygonApi(
		this IServiceCollection services,
		string sectionPath)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(sectionPath);

		services
			.AddOptions<PolygonOptions>()
			.BindConfiguration(sectionPath);
		services.DoAddPolygonClient();

		return services;
	}

	/// <summary>
	/// Registers the <see cref="PolygonApi"/> infrastructure, with the option values set in the provided <paramref
	/// name="configureOptions"/> function.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to</param>
	/// <param name="configureOptions">The action used to configure the options</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained</returns>
	public static IServiceCollection AddPolygonApi(
		this IServiceCollection services,
		Action<PolygonOptions> configureOptions)
	{
		Guard.IsNotNull(services);
		Guard.IsNotNull(configureOptions);

		services.Configure(configureOptions);
		services.DoAddPolygonClient();

		return services;
	}

	private static void DoAddPolygonClient(this IServiceCollection services)
	{
		services.PostConfigure<PolygonOptions>(o =>
		{
			Guard.IsNotNull(o.ApiKey, "Polygon.io ApiKey");
		});

		services.AddTransient<PolygonMessageHandler>();
		services
			.AddRefitClient<IPolygonApiRefit>(settings: new()
			{
				ContentSerializer = new SystemTextJsonContentSerializer(PolygonApi.DefaultSerializerOptions),
				UrlParameterFormatter = new UrlParameterFormatter(),
			})
			.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.polygon.io/"))
			.ConfigurePrimaryHttpMessageHandler(() =>
				new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All, })
			.AddHttpMessageHandler<PolygonMessageHandler>();

		services.AddSingleton(sp => new PolygonApi(
			sp.GetRequiredService<IPolygonApiRefit>(),
			sp.GetRequiredService<IOptions<PolygonOptions>>()));
	}
}
