using System.Runtime.Serialization;

namespace Regular.Polygon;

/// <summary>
/// Order results based on a sort field.
/// </summary>
public enum SortOrder
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	None,
	[EnumMember(Value = "Asc")] Ascending,
	[EnumMember(Value = "Desc")] Descending,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
