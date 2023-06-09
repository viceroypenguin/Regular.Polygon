namespace Regular.Polygon.SocketManager;

internal sealed partial class PolygonSocketManager
{
#pragma warning disable CA1001 // false positive, see https://github.com/dotnet/roslyn-analyzers/issues/6151
	private readonly struct Semaphore : IDisposable
#pragma warning restore CA1001
	{
		private readonly SemaphoreSlim _sem;

		public Semaphore(int initialCount)
		{
			_sem = new SemaphoreSlim(initialCount);
		}

		public IDisposable Wait()
		{
			_sem.Wait();
			return new Disposable(_sem);
		}

		public async Task<IDisposable> WaitAsync()
		{
			await _sem.WaitAsync().ConfigureAwait(false);
			return new Disposable(_sem);
		}

		public void Dispose() => _sem.Dispose();

		private sealed class Disposable : IDisposable
		{
			private readonly SemaphoreSlim _sem;
			private bool _disposed;
			public Disposable(SemaphoreSlim s)
			{
				_sem = s;
			}

			public void Dispose()
			{
				if (!_disposed)
					_sem.Release();
				_disposed = true;
			}
		}
	}
}
