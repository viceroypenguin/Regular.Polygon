using System.Threading.Channels;

namespace Regular.Polygon.SocketManager;

internal sealed partial class PolygonSocketManager
{
	private sealed class LiveMessagesEnumerable<T> : IAsyncEnumerable<T>, IChannelContainer
	{
		private readonly PolygonSocketManager _socketManager;
		private readonly string _key;
		private readonly int _channelSize;

		private readonly Semaphore _lock = new(1);
		private readonly List<Queue<T>> _buffers = new();

		public LiveMessagesEnumerable(PolygonSocketManager socketManager, string key, int size)
		{
			_socketManager = socketManager;
			_key = key;
			_channelSize = size;
		}

		public Channel<JsonElement>? Channel { get; set; }

		private async Task<Queue<T>> Subscribe(CancellationToken token)
		{
			using var __disp = await _lock.WaitAsync().ConfigureAwait(false);
			if (Channel == null)
			{
				Channel = System.Threading.Channels.Channel.CreateBounded<JsonElement>(_channelSize);
				await _socketManager.Subscribe(_key, token).ConfigureAwait(false);
			}

			var buffer = new Queue<T>(_channelSize);
			_buffers.Add(buffer);
			return buffer;
		}

		private async Task Unsubscribe(Queue<T> buffer)
		{
			using var __disp = await _lock.WaitAsync().ConfigureAwait(false);
			if (!_buffers.Remove(buffer))
				return;

			if (_buffers.Count == 0)
			{
				await _socketManager.Unsubscribe(_key).ConfigureAwait(false);
				Channel = null;
			}
		}

		private void Enqueue(T m)
		{
			foreach (var b in _buffers)
				b.Enqueue(m);
		}

		public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken token) =>
			new LiveMessagesEnumerator(this, token);

		private sealed class LiveMessagesEnumerator : IAsyncEnumerator<T>
		{
			private readonly LiveMessagesEnumerable<T> _enumerable;
			private readonly CancellationToken _token;
			private Queue<T>? _queue = default!;
			private int _state;

			public LiveMessagesEnumerator(LiveMessagesEnumerable<T> enumerable, CancellationToken token)
			{
				token.ThrowIfCancellationRequested();

				_enumerable = enumerable;
				_token = token;
			}

			public async ValueTask<bool> MoveNextAsync()
			{
				if (_state == -1)
					return false;

				if (_state == 0)
				{
					_queue = await _enumerable.Subscribe(_token).ConfigureAwait(false);
					_state = 1;
				}

				using var __disp = await _enumerable._lock.WaitAsync().ConfigureAwait(false);

				if (_queue!.Count > 0)
				{
					Current = _queue.Dequeue();
					return true;
				}

				if (_enumerable.Channel == null)
				{
					await DisposeAsync().ConfigureAwait(false);
					return false;
				}

				var ch = _enumerable.Channel.Reader;
				if (!await ch.WaitToReadAsync().ConfigureAwait(false))
				{
					await DisposeAsync().ConfigureAwait(false);
					return false;
				}

				_ = ch.TryRead(out var el);
				var m = el.Deserialize<T>()!;

				_enumerable.Enqueue(m);

				Current = _queue.Dequeue();
				return true;
			}

			public T Current { get; private set; } = default!;

			public async ValueTask DisposeAsync()
			{
				if (_queue != null)
				{
					await _enumerable.Unsubscribe(_queue).ConfigureAwait(false);

					_state = -1;
					_queue = null;
					Current = default!;
				}
			}
		}
	}
}
