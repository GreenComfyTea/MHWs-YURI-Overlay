namespace YURI_Overlay;

internal sealed class Debouncer : IDisposable
{
	private CancellationTokenSource? _cancellationTokenSource;

	public void Debounce(Action action, int delayMilliseconds)
	{
		_cancellationTokenSource?.Cancel(); // Cancel any previously scheduled task
		_cancellationTokenSource = new CancellationTokenSource();

		var token = _cancellationTokenSource.Token;

		Task.Delay(delayMilliseconds, token)
			.ContinueWith(
				task =>
				{
					if (!task.IsCanceled)
						action();
				},
				TaskScheduler.Default
			);
	}

	public void Dispose()
	{
		_cancellationTokenSource?.Cancel();
		_cancellationTokenSource?.Dispose();
		_cancellationTokenSource = null;
	}
}
