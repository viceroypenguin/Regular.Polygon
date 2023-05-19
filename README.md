# Regular.Polygon

| Name            | Status | History |
| :---            | :---   | :---    |
| GitHub Actions  | ![Build](https://github.com/viceroypenguin/Regular.Polygon/actions/workflows/build.yml/badge.svg) | [![GitHub Actions Build History](https://buildstats.info/github/chart/viceroypenguin/Regular.Polygon?branch=master&includeBuildsFromPullRequest=false)](https://github.com/viceroypenguin/Regular.Polygon/actions) |

[![GitHub release](https://img.shields.io/github/release/viceroypenguin/Regular.Polygon.svg)](https://github.com/viceroypenguin/Regular.Polygon/releases/)
[![GitHub license](https://img.shields.io/github/license/viceroypenguin/Regular.Polygon.svg)](https://github.com/viceroypenguin/Regular.Polygon/blob/master/license.txt) 
[![GitHub issues](https://img.shields.io/github/issues/viceroypenguin/Regular.Polygon.svg)](https://github.com/viceroypenguin/Regular.Polygon/issues/) 
[![GitHub issues-closed](https://img.shields.io/github/issues-closed/viceroypenguin/Regular.Polygon.svg)](https://github.com/viceroypenguin/Regular.Polygon/issues?q=is%3Aissue+is%3Aclosed) 
---

## What is Regular.Polygon?
Regular.Polygon is a library for interacting with [Polygon](https://www.polygon.io/)'s financial information APIs. See
their documentation [here](https://polygon.io/docs/). It is supported for .net 6.0+.

### Where can I get it?
Regular.Polygon is available at [nuget.org](https://www.nuget.org/packages/Regular.Polygon).

Package Manager `PM > Install-Package Regular.Polygon`

### How it works?
You can make all calls to Polygon's Rest API via the `IPolygonApi` interface.

#### Initialize Regular.Polygon
Register Regular.Polygon with the application services:

```c#
services.AddPolygonApi(<key>);
```
where `<key>` is an API key provided by Polygon.

#### Use `IPolygonApi`
Receive an instance of the `IPolygonApi` interface from the application services
and make API calls via this instance.

```c#
public class StockService
{
	private readonly IPolygonApi _polygonApi;

	public StockService(IPolygonApi polygonApi)
	{
		_polygonApi = polygonApi;
	}

	public async Task DoSomething()
	{
		var marketStatus = await _polygonApi.GetMarketStatus();
	}
}
```
