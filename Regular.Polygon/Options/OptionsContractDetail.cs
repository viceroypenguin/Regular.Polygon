using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Regular.Polygon.Options;

/// <summary>
/// The exercise style of a contract
/// </summary>
public enum ExerciseStyle
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None = 0,

	[EnumMember(Value = "american")] American,
	[EnumMember(Value = "european")] European,
	[EnumMember(Value = "bermudan")] Bermudan,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Detailed information about a single options contract
/// </summary>
/// <param name="Ticker">The ticker for the option contract.</param>
/// <param name="UnderlyingTicker">The underlying ticker that the option contract relates to.</param>
/// <param name="Cfi">The 6 letter CFI code of the contract (defined in <a
/// href="https://en.wikipedia.org/wiki/ISO_10962">ISO 10962</a>)</param>
/// <param name="ContractType">The type of contract. Can be "put", "call", or in some rare cases, "other".</param>
/// <param name="ExerciseStyle">The exercise style of this contract.</param>
/// <param name="ExpirationDate">The contract's expiration date</param>
/// <param name="PrimaryExchange">The MIC code of the primary exchange that this contract is listed on.</param>
/// <param name="SharesPerContract">The number of shares per contract for this contract.</param>
/// <param name="StrikePrice">The strike price of the option contract.</param>
[ExcludeFromCodeCoverage]
public sealed record OptionsContractDetail(
	[property: JsonPropertyName("ticker")]
	string Ticker,
	[property: JsonPropertyName("underlying_ticker")]
	string UnderlyingTicker,
	[property: JsonPropertyName("cfi")]
	string Cfi,
	[property: JsonPropertyName("contract_type")]
	string ContractType,
	[property: JsonPropertyName("exercise_style")]
	ExerciseStyle ExerciseStyle,
	[property: JsonPropertyName("expiration_date")]
	DateOnly ExpirationDate,
	[property: JsonPropertyName("primary_exchange")]
	string PrimaryExchange,
	[property: JsonPropertyName("shares_per_contract")]
	int SharesPerContract,
	[property: JsonPropertyName("strike_price")]
	decimal StrikePrice)
{
	/// <summary>
	/// The correction number for this option contract.
	/// </summary>
	[JsonPropertyName("correction")]
	public int? Correction { get; init; }

	/// <summary>
	/// If an option contract has additional underlyings or deliverables associated with it, they will appear here. See
	/// <a href="https://www.optionseducation.org/referencelibrary/faq/splits-mergers-spinoffs-bankruptcies">here</a>
	/// for some examples of what might cause a contract to have additional underlyings.
	/// </summary>
	[JsonPropertyName("additional_underlyings")]
	public IReadOnlyList<AdditionalUnderlying>? AdditionalUnderlyings { get; init; }
}

/// <summary>
/// If an option contract has additional underlyings or deliverables associated with it, they will appear here. See <a
/// href="https://www.optionseducation.org/referencelibrary/faq/splits-mergers-spinoffs-bankruptcies">here</a> for some
/// examples of what might cause a contract to have additional underlyings.
/// </summary>
/// <param name="Type">The type of the additional underlying asset, either equity or currency.</param>
/// <param name="Underlying">The name of the additional underlying asset.</param>
/// <param name="Amount">The number of shares per contract of the additional underlying, or the cash-in-lieu amount of
/// the currency.</param>
[ExcludeFromCodeCoverage]
public sealed record AdditionalUnderlying(
	[property: JsonPropertyName("type")]
	string Type,
	[property: JsonPropertyName("underlying")]
	string Underlying,
	[property: JsonPropertyName("amount")]
	decimal Amount);
