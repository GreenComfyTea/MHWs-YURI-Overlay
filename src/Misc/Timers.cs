using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal static class Timers
{
	public static Timer SetInterval(Action method, int delayInMilliseconds)
	{
		Timer timer = new(delayInMilliseconds);

		timer.Elapsed += (source, eventArgs) => method();
		timer.Enabled = true;
		timer.Start();

		// Returns a stop handle which can be used for stopping
		// the timer, if required
		return timer;
	}

	public static Timer SetTimeout(Action method, int delayInMilliseconds)
	{
		//return Task.Delay(delayInMilliseconds).ContinueWith((_) => method());

		Timer timer = new(delayInMilliseconds);

		timer.Elapsed += (source, eventArgs) => method();
		timer.AutoReset = false;
		timer.Enabled = true;
		timer.Start();

		// Returns a stop handle which can be used for stopping
		// the timer, if required
		return timer;
	}
}