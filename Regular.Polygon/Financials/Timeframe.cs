using System.Runtime.Serialization;

namespace Regular.Polygon.Financials;

/// <summary>
/// Financial Report Timeframes
/// </summary>
public enum Timeframe
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "annual")] Annual,
	[EnumMember(Value = "quarterly")] Quarterly,
	[EnumMember(Value = "ttm")] TrailingTwelveMonths,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
