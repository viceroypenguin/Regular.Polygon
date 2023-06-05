using System.Text.Json.Serialization;

namespace Regular.Polygon;

/// <inheritdoc/>
internal sealed class UnixDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
	/// <inheritdoc/>
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.TokenType == JsonTokenType.Number ? DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()) :
		reader.TokenType == JsonTokenType.String ? DateTimeOffset.Parse(reader.GetString()!, formatProvider: null) :
		throw new InvalidOperationException("Unable to parse DateTimeOffset");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
		writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
}
