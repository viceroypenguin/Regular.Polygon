using System.Buffers;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;

namespace Regular.Polygon.SocketManager;

internal sealed partial class PolygonSocketManager : IAsyncDisposable
{
	private readonly PolygonDataStatus _server;
	private readonly string _url;
	private readonly string _apiKey;

	public PolygonSocketManager(PolygonDataStatus server, string url, string apiKey)
	{
		_server = server;
		_url = url;
		_apiKey = apiKey;
	}

	private readonly ConcurrentDictionary<string, IChannelContainer> _channels = new(StringComparer.OrdinalIgnoreCase);
	private readonly ConcurrentDictionary<string, bool> _subscriptions = new(StringComparer.OrdinalIgnoreCase);
	private readonly Semaphore _lock = new(1);
	private ClientWebSocket? _socket;
	private Task? _runningLoop;
	private CancellationTokenSource? _loopTerminator;

	public IAsyncEnumerable<T> GetEventsForKey<T>(string key, int maxCapacity) =>
		(_channels.GetOrAdd(key, k => new LiveMessagesEnumerable<T>(this, k, maxCapacity))
			as IAsyncEnumerable<T>)!;

	public async ValueTask DisposeAsync()
	{
		using var __disp = await _lock.WaitAsync().ConfigureAwait(false);
		await TerminateLoop().ConfigureAwait(false);
		_lock.Dispose();
	}

	private async Task Subscribe(string key, CancellationToken token)
	{
		if (key.StartsWith("status.", StringComparison.Ordinal))
			return;

		if (_subscriptions.GetValueOrDefault(key))
			return;

		using var __disp = await _lock.WaitAsync().ConfigureAwait(false);

		if (_subscriptions.GetValueOrDefault(key))
			return;

		await StartLoop().ConfigureAwait(false);

		var statusEnumerable = GetStatusMessages(key);
#pragma warning disable CA2007 // false positive, see https://github.com/dotnet/roslyn-analyzers/issues/5712
		await using var iterator = statusEnumerable
			.ConfigureAwait(false)
			.WithCancellation(token)
			.GetAsyncEnumerator();
#pragma warning restore CA2007

		var statusMessageTask = iterator.MoveNextAsync();

		await SendMessage(_socket!, new { action = "subscribe", @params = key, }, token).ConfigureAwait(false);

		if (!await statusMessageTask
			|| !iterator.Current.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
		{
			ThrowHelper.ThrowInvalidOperationException($"Failure subscribing to {key}...");
		}

		_subscriptions[key] = true;
	}

	private async Task Unsubscribe(string key)
	{
		if (key.StartsWith("status.", StringComparison.Ordinal))
			return;

		if (!_subscriptions.GetValueOrDefault(key))
			return;

		using var __disp = await _lock.WaitAsync().ConfigureAwait(false);

		if (!_subscriptions.GetValueOrDefault(key))
			return;

		var statusEnumerable = GetStatusMessages(key);
#pragma warning disable CA2007 // false positive, see https://github.com/dotnet/roslyn-analyzers/issues/5712
		await using var iterator = statusEnumerable
			.ConfigureAwait(false)
			.GetAsyncEnumerator();
#pragma warning restore CA2007

		var statusMessageTask = iterator.MoveNextAsync();

		await SendMessage(_socket!, new { action = "unsubscribe", @params = key, }, CancellationToken.None).ConfigureAwait(false);

		if (!await statusMessageTask
			|| !iterator.Current.Status.Equals("success", StringComparison.OrdinalIgnoreCase))
		{
			ThrowHelper.ThrowInvalidOperationException($"Failure unsubscribing to {key}...");
		}

		_ = _subscriptions.Remove(key, out var _);

		if (_subscriptions.IsEmpty)
		{
			await TerminateLoop().ConfigureAwait(false);
		}
	}

	private IAsyncEnumerable<StatusMessage> GetStatusMessages(string key) =>
		(_channels.GetOrAdd($"status.{key}", k => new LiveMessagesEnumerable<StatusMessage>(this, k, 4))
			as IAsyncEnumerable<StatusMessage>)!;

	private async Task StartLoop()
	{
		async Task<ClientWebSocket> InitializeConnection()
		{
			var socket = new ClientWebSocket();
			var url = _server switch
			{
				PolygonDataStatus.Delayed => "wss://delayed.polygon.io/",
				PolygonDataStatus.Live => "wss://socket.polygon.io/",
				_ => ThrowHelper.ThrowInvalidOperationException<string>("invalid server type."),
			};

			await socket.ConnectAsync(new Uri($"{url}{_url}"), CancellationToken.None).ConfigureAwait(false);

			var messages = await ReceiveMessages(socket, CancellationToken.None).ConfigureAwait(false);
			var statuses = messages.Deserialize<List<StatusMessage>>();
			if (statuses == null
				|| statuses.Count != 1
				|| statuses[0].Status != "connected")
			{
				ThrowHelper.ThrowInvalidOperationException("Failure connecting.");
			}

			await SendMessage(socket, new { action = "auth", @params = _apiKey, }, CancellationToken.None).ConfigureAwait(false);
			messages = await ReceiveMessages(socket, CancellationToken.None).ConfigureAwait(false);
			statuses = messages.Deserialize<List<StatusMessage>>();
			if (statuses == null
				|| statuses.Count != 1
				|| statuses[0].Status != "auth_success")
			{
				ThrowHelper.ThrowInvalidOperationException("Failure authenticating.");
			}

			return socket;
		}

		if (_runningLoop != null)
			return;

		var socket = _socket = await InitializeConnection().ConfigureAwait(false);
		_loopTerminator = new();
		_runningLoop = Task.Run(() => Loop(socket, _loopTerminator.Token));
	}

	private async Task Loop(ClientWebSocket socket, CancellationToken token)
	{
		while (true)
		{
			try
			{
				var messages = await ReceiveMessages(socket, token).ConfigureAwait(false);
				foreach (var m in messages.RootElement.EnumerateArray())
				{
					var key = GetKey(m);
					var container = _channels.GetValueOrDefault(key);
					if (container == null) continue;

					var channel = container.Channel;
					if (channel == null) continue;

					if (!channel.Writer.TryWrite(m))
					{
						// fail due to out of space??
					}
				}
			}
			catch (OperationCanceledException) when (token.IsCancellationRequested)
			{
				break;
			}
			catch
			{
				// handle failure??
				throw;
			}
		}
	}

	private async Task TerminateLoop()
	{
#if NET8_0_OR_GREATER
		await _loopTerminator!.CancelAsync().ConfigureAwait(false);
#else
		_loopTerminator!.Cancel();
#endif

		await _runningLoop!.ConfigureAwait(false);

		_runningLoop = null;
		_loopTerminator.Dispose();
		_loopTerminator = null;
		_socket!.Dispose();
		_socket = null;
	}

	private static string GetKey(JsonElement message) =>
		message.GetProperty("ev"u8).GetString() switch
		{
			"status" => GetStatusKey(message),
			var ev when ev is "A" or "AM" or "T" =>
				$"{ev}.{message.GetProperty("sym"u8).GetString()}",
			_ => throw new InvalidOperationException("Unknown Message Type"),
		};

	private static readonly Regex s_statusMessageRegex = StatusMessageRegex();
	private static string GetStatusKey(JsonElement message)
	{
		var txt = message.GetProperty("message"u8).GetString()!;
		var m = s_statusMessageRegex.Match(txt);
		var symbol = m.Groups["symbol"].Value;
		return $"status.{symbol}";
	}

	private static async Task<JsonDocument> ReceiveMessages(ClientWebSocket socket, CancellationToken token)
	{
		using var memory = MemoryPool<byte>.Shared.Rent(16384);
		var buffer = memory.Memory;
		buffer.Span.Clear();

		_ = await socket.ReceiveAsync(buffer, token).ConfigureAwait(false);
		return ParseDocument(buffer);
	}

	private static JsonDocument ParseDocument(ReadOnlyMemory<byte> buffer)
	{
		var span = buffer.Span;
		span = span[..span.IndexOf((byte)0)];
		return JsonSerializer.Deserialize<JsonDocument>(span)!;
	}

	private static Task SendMessage<T>(ClientWebSocket socket, T obj, CancellationToken token) =>
		socket.SendAsync(
			JsonSerializer.SerializeToUtf8Bytes(obj),
			WebSocketMessageType.Text,
			endOfMessage: true,
			token);

#if NET7_0_OR_GREATER
	[GeneratedRegex("^(un)?subscribed to: (?<symbol>[\\w\\.]+)$", RegexOptions.Compiled)]
	private static partial Regex StatusMessageRegex();
#else
	private static Regex StatusMessageRegex() =>
		new("^(un)?subscribed to: (?<symbol>[\\w\\.]+)$", RegexOptions.Compiled);
#endif
}
