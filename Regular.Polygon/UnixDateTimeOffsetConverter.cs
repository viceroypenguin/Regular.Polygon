using System.Text.Json.Serialization;

namespace Regular.Polygon;

/// <inheritdoc/>
internal sealed class UnixMillisecondDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
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

/// <inheritdoc/>
internal sealed class UnixNanosecondDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
	private static DateTimeOffset FromUnixTimeNanoseconds(long nanoSeconds)
	{
		var dto = DateTimeOffset.FromUnixTimeMilliseconds(nanoSeconds / 1_000_000);
		dto = dto.AddTicks(nanoSeconds % 1_000_000 / 100);
		return dto;
	}

	/// <inheritdoc/>
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.TokenType == JsonTokenType.Number ? FromUnixTimeNanoseconds(reader.GetInt64()) :
		reader.TokenType == JsonTokenType.String ? DateTimeOffset.Parse(reader.GetString()!, formatProvider: null) :
		throw new InvalidOperationException("Unable to parse DateTimeOffset");

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
		writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
}
