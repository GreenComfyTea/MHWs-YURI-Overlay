using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class LocalizationWatcher : IDisposable
{
	private readonly FileSystemWatcher? _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = [];

	private bool _disabled;
	private Timer? _delayedEnableTimer;

	private readonly bool _stub;

	public LocalizationWatcher(bool stub)
	{
		this._stub = stub;
	}

	public LocalizationWatcher()
	{
		LogManager.Info("[LocalizationWatcher] Initializing...");

		try
		{
			this._watcher = new FileSystemWatcher(Constants.LocalizationsPath);

			this._watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;

			this._watcher.Changed += this.OnLocalizationFileChanged;
			this._watcher.Created += this.OnLocalizationFileCreated;
			this._watcher.Renamed += this.OnLocalizationFileRenamed;
			this._watcher.Deleted += this.OnLocalizationFileDeleted;
			this._watcher.Error += this.OnLocalizationFileError;

			this._watcher.Filter = "*.json";
			this._watcher.EnableRaisingEvents = true;

			LogManager.Info("[LocalizationWatcher] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~LocalizationWatcher()
	{
		this.Dispose();
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
			LogManager.Info("[LocalizationWatcher] Disposing...");
		}

		this._delayedEnableTimer?.Dispose();
		this._watcher?.Dispose();

		if(!this._stub)
		{
			LogManager.Info("[LocalizationWatcher] Disposed!");
		}
	}

	private void OnLocalizationFileChanged(object? sender, FileSystemEventArgs e)
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

			if(!this._lastEventTimes.ContainsKey(name))
			{
				this._lastEventTimes[name] = DateTime.MinValue;
			}

			if(eventTime.Ticks - this._lastEventTimes[name].Ticks < Constants.DuplicateEventThresholdTicks)
			{
				return;
			}

			LogManager.Info($"Localization \"{name}\": Changed.");

			if(LocalizationManager.Instance.Localizations.ContainsKey(name))
			{
				return;
			}

			this._lastEventTimes[name] = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileCreated(object? sender, FileSystemEventArgs e)
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
				LogManager.Warn("Invalid localization name.");

				return;
			}

			LogManager.Info($"Localization \"{name}\": Created.");

			LocalizationManager.Instance.InitializeLocalization(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileDeleted(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileRenamed(object? sender, RenamedEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var oldName = Path.GetFileNameWithoutExtension(e.OldName);
			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileError(object? sender, ErrorEventArgs e)
	{
		if(this._disabled)
		{
			return;
		}

		LogManager.Info("[LocalizationWatcher] Unknown error.");
	}
}