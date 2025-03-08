namespace YURI_Overlay;

internal partial class LocalizationWatcher : IDisposable
{
	private readonly FileSystemWatcher _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = [];

	private bool _disabled = false;

	public LocalizationWatcher()
	{
		try
		{
			LogManager.Info("[LocalizationWatcher] Initializing...");

			_watcher = new FileSystemWatcher(Constants.LocalizationsPath);

			_watcher.NotifyFilter = NotifyFilters.Attributes
								 | NotifyFilters.CreationTime
								 | NotifyFilters.FileName
								 | NotifyFilters.LastWrite
								 | NotifyFilters.Security
								 | NotifyFilters.Size;

			_watcher.Changed += OnLocalizationFileChanged;
			_watcher.Created += OnLocalizationFileCreated;
			_watcher.Renamed += OnLocalizationFileRenamed;
			_watcher.Deleted += OnLocalizationFileDeleted;
			_watcher.Error += OnLocalizationFileError;

			_watcher.Filter = "*.json";
			_watcher.EnableRaisingEvents = true;

			LogManager.Info("[LocalizationWatcher] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~LocalizationWatcher()
	{
		Dispose();
	}

	public void Enable()
	{
		_disabled = false;
	}

	public void DelayedEnable()
	{
		Timers.SetTimeout(Enable, Constants.ReenableWatcherDelayMilliseconds);
	}

	public void Disable()
	{
		_disabled = true;
	}

	public void Dispose()
	{
		LogManager.Info("[LocalizationWatcher] Disposing...");

		_watcher.Dispose();

		LogManager.Info("[LocalizationWatcher] Disposed!");
	}

	private void OnLocalizationFileChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);
			if(name == null)
			{
				return;
			}

			var eventTime = File.GetLastWriteTime(e.FullPath);
			if(!_lastEventTimes.ContainsKey(name))
			{
				_lastEventTimes[name] = DateTime.MinValue;
			}

			if(eventTime.Ticks - _lastEventTimes[name].Ticks < Constants.DuplicateEventThresholdTicks)
			{
				return;
			}

			LogManager.Info($"Localization \"{name}\": Changed.");

			if(LocalizationManager.Instance.Localizations.ContainsKey(name))
			{
				return;
			}

			_lastEventTimes[name] = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileCreated(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{name}\": Created.");

			LocalizationManager.Instance.InitializeLocalization(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileDeleted(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileRenamed(object sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var oldName = Path.GetFileNameWithoutExtension(e.OldName);
			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnLocalizationFileError(object sender, ErrorEventArgs e)
	{
		if(_disabled) return;

		LogManager.Info("[LocalizationWatcher] Unknown error.");
	}
}
