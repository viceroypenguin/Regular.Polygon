using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Refit;

namespace Regular.Polygon.Ticker;

/// <summary>
/// Asset class options for the Ticker Types api
/// </summary>
public enum AssetClass
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "stocks")] Stocks,
	[EnumMember(Value = "options")] Options,
	[EnumMember(Value = "crypto")] Crypto,
	[EnumMember(Value = "fx")] Fx,
	[EnumMember(Value = "indices")] Indices,
	[EnumMember(Value = "otc")] OverTheCounter,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Locale options for the Ticker Types api
/// </summary>
public enum Locale
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "us")] Us,
	[EnumMember(Value = "global")] Global,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Request class to hold parameters for the Ticker Types api
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class TickerTypeRequest
{
	/// <summary>
	/// Filter by asset class.
	/// </summary>
	[AliasAs("asset_class")]
	public AssetClass? AssetClass { get; set; }

	/// <summary>
	/// Filter by locale.
	/// </summary>
	[AliasAs("locale")]
	public Locale? Locale { get; set; }
}

/// <summary>
/// A description of an asset class supported by Polygon
/// </summary>
/// <param name="AssetClass">An identifier for a group of similar financial instruments.</param>
/// <param name="Locale">An identifier for a geographical location.</param>
/// <param name="Code">A code used by Polygon.io to refer to this ticker type.</param>
/// <param name="Description">A short description of this ticker type.</param>
[ExcludeFromCodeCoverage]
public sealed record TickerType(
	[property: JsonPropertyName("asset_class")]
	AssetClass AssetClass,
	Locale Locale,
	string Code,
	string Description);
