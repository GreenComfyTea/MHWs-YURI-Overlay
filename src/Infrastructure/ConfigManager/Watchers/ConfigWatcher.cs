using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal class ConfigWatcher : IDisposable
{
	private readonly FileSystemWatcher _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = [];

	private bool _disabled;
	private Timer _delayedEnableTimer;

	public ConfigWatcher()
	{
		try
		{
			LogManager.Info("[ConfigWatcher] Initializing...");

			_watcher = new FileSystemWatcher(Constants.ConfigsPath);

			_watcher.NotifyFilter = NotifyFilters.Attributes
									| NotifyFilters.CreationTime
									| NotifyFilters.FileName
									| NotifyFilters.LastWrite
									| NotifyFilters.Security
									| NotifyFilters.Size;

			_watcher.Changed += OnConfigFileChanged;
			_watcher.Created += OnConfigFileCreated;
			_watcher.Renamed += OnConfigFileRenamed;
			_watcher.Deleted += OnConfigFileDeleted;
			_watcher.Error += OnConfigFileError;

			_watcher.Filter = "*.json";
			_watcher.EnableRaisingEvents = true;

			LogManager.Info("[ConfigWatcher] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~ConfigWatcher()
	{
		LogManager.Info("[ConfigWatcher] Disposing...");

		Dispose();

		LogManager.Info("[ConfigWatcher] Disposed!");
	}

	public void Enable()
	{
		_disabled = false;
		_delayedEnableTimer?.Dispose();
		_delayedEnableTimer = null;

		LogManager.Info("[LocalizationWatcher] Enabled!");
	}

	public void DelayedEnable()
	{
		_delayedEnableTimer?.Dispose();
		_delayedEnableTimer = Timers.SetTimeout(Enable, Constants.ReenableWatcherDelayMilliseconds);

		LogManager.Info("[LocalizationWatcher] Will enable after a delay...");
	}

	public void Disable()
	{
		_disabled = true;
		_delayedEnableTimer?.Dispose();

		LogManager.Info("[LocalizationWatcher] Temporarily disabled!");
	}

	public void Dispose()
	{
		LogManager.Info("[ConfigWatcher] Disposing...");

		_watcher.Dispose();

		LogManager.Info("[ConfigWatcher] Disposed!");
	}

	private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			if(name is null) return;

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(!_lastEventTimes.TryGetValue(name, out var lastEventTime))
			{
				lastEventTime = eventTime;
				_lastEventTimes[name] = lastEventTime;
				LogManager.Info($"Config \"{name}\": Changed.");
				return;
			}

			if(eventTime.Ticks - lastEventTime.Ticks < Constants.DuplicateEventThresholdTicks) return;

			LogManager.Info($"Config \"{name}\": Changed.");

			_lastEventTimes[name] = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileCreated(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{name}\": Created.");

			ConfigManager.Instance.InitializeConfig(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileRenamed(object sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var oldName = Path.GetFileNameWithoutExtension(e.OldName);
			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileError(object sender, ErrorEventArgs e)
	{
		if(_disabled) return;

		LogManager.Info("[ConfigWatcher] Unknown error.");
	}
}