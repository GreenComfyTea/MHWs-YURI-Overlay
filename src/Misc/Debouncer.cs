namespace YURI_Overlay;

internal sealed class Debouncer : IDisposable
{
	private CancellationTokenSource? _cancellationTokenSource;

	public void Dispose()
	{
		this._cancellationTokenSource?.Cancel();
		this._cancellationTokenSource?.Dispose();
		this._cancellationTokenSource = null;
	}

	public void Debounce(Action action, int delayMilliseconds)
	{
		this._cancellationTokenSource?.Cancel(); // Cancel any previously scheduled task
		this._cancellationTokenSource = new CancellationTokenSource();

		var token = this._cancellationTokenSource.Token;

		Task.Delay(delayMilliseconds, token)
			.ContinueWith(
				task =>
				{
					if(!task.IsCanceled)
					{
						action();
					}
				},
				TaskScheduler.Default
			);
	}
}