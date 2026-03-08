using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class ConfigWatcher : IDisposable
{
	private readonly FileSystemWatcher? _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = [];

	private bool _disabled;
	private Timer? _delayedEnableTimer;

	private readonly bool _stub;

	public ConfigWatcher(bool stub)
	{
		this._stub = stub;
	}

	public ConfigWatcher()
	{
		LogManager.Info("[ConfigWatcher] Initializing...");

		try
		{
			this._watcher = new FileSystemWatcher(Constants.ConfigsPath);

			this._watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;

			this._watcher.Changed += this.OnConfigFileChanged;
			this._watcher.Created += this.OnConfigFileCreated;
			this._watcher.Renamed += this.OnConfigFileRenamed;
			this._watcher.Deleted += this.OnConfigFileDeleted;
			this._watcher.Error += this.OnConfigFileError;

			this._watcher.Filter = "*.json";
			this._watcher.EnableRaisingEvents = true;

			LogManager.Info("[ConfigWatcher] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~ConfigWatcher()
	{
		if(!this._stub)
		{
			LogManager.Info("[ConfigWatcher] Disposing...");
		}

		this.Dispose();

		if(!this._stub)
		{
			LogManager.Info("[ConfigWatcher] Disposed!");
		}
	}

	public void Enable()
	{
		this._disabled = false;
		this._delayedEnableTimer?.Dispose();
		this._delayedEnableTimer = null;

		if(!this._stub)
		{
			LogManager.Info("[LocalizationWatcher] Enabled!");
		}
	}

	public void DelayedEnable()
	{
		this._delayedEnableTimer?.Dispose();
		this._delayedEnableTimer = Timers.SetTimeout(this.Enable, Constants.ReenableWatcherDelayMilliseconds);

		if(!this._stub)
		{
			LogManager.Info("[LocalizationWatcher] Will enable after a delay...");
		}
	}

	public void Disable()
	{
		this._disabled = true;
		this._delayedEnableTimer?.Dispose();

		if(!this._stub)
		{
			LogManager.Info("[LocalizationWatcher] Temporarily disabled!");
		}
	}

	public void Dispose()
	{
		if(!this._stub)
		{
			LogManager.Info("[ConfigWatcher] Disposing...");
		}

		this._delayedEnableTimer?.Dispose();
		this._watcher?.Dispose();

		if(!this._stub)
		{
			LogManager.Info("[ConfigWatcher] Disposed!");
		}
	}

	private void OnConfigFileChanged(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var name = Path.GetFileNameWithoutExtension(e.Name);

			if(name is null)
			{
				return;
			}

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(!this._lastEventTimes.TryGetValue(name, out var lastEventTime))
			{
				lastEventTime = eventTime;
				this._lastEventTimes[name] = lastEventTime;
				LogManager.Info($"Config \"{name}\": Changed.");

				return;
			}

			if(eventTime.Ticks - lastEventTime.Ticks < Constants.DuplicateEventThresholdTicks)
			{
				return;
			}

			LogManager.Info($"Config \"{name}\": Changed.");

			this._lastEventTimes[name] = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileCreated(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var name = Path.GetFileNameWithoutExtension(e.Name);

			if(name is null)
			{
				LogManager.Warn("Invalid config name.");

				return;
			}

			LogManager.Info($"Config \"{name}\": Created.");

			ConfigManager.Instance.InitializeConfig(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileDeleted(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileRenamed(object? sender, RenamedEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var oldName = Path.GetFileNameWithoutExtension(e.OldName);
			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileError(object? sender, ErrorEventArgs e)
	{
		if(this._disabled)
		{
			return;
		}

		LogManager.Info("[ConfigWatcher] Unknown error.");
	}
}