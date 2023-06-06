using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Regular.Polygon;

/// <summary>
/// A formatter that will print dates in ISO format
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class UrlParameterFormatter : DefaultUrlParameterFormatter
{
	/// <inheritdoc />
	public override string? Format(object? parameterValue, ICustomAttributeProvider attributeProvider, Type type)
	{
		if (parameterValue is DateOnly date)
		{
			return FormattableString.Invariant($"{date:yyyy-MM-dd}");
		}

		if (parameterValue is DateTimeOffset dto)
		{
			var ns = dto.ToUnixTimeMilliseconds() * 1_000_000;
			ns += (dto.Ticks % 10_000L) * 100;
			return FormattableString.Invariant($"{ns}");
		}

		return base.Format(parameterValue, attributeProvider, type);
	}
}
