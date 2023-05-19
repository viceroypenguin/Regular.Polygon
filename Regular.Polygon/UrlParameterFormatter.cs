using System.Reflection;

namespace Regular.Polygon;

/// <summary>
/// A formatter that will print dates in ISO format
/// </summary>
public sealed class UrlParameterFormatter : DefaultUrlParameterFormatter
{
	/// <inheritdoc />
	public override string? Format(object? parameterValue, ICustomAttributeProvider attributeProvider, Type type)
	{
		if (parameterValue?.GetType() == typeof(DateOnly))
			return FormattableString.Invariant($"{parameterValue:yyyy-MM-dd}");
		return base.Format(parameterValue, attributeProvider, type);
	}
}
