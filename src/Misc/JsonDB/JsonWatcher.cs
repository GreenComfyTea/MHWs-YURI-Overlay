using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class JsonWatcher<T> : IDisposable where T : class, new()
{
	private readonly JsonDatabase<T> _jsonDatabaseInstance;
	private readonly FileSystemWatcher? _watcher;

	private bool _disabled;
	private DateTime _lastEventTime = DateTime.MinValue;
	private Timer? _delayedEnableTimer;

	private readonly bool _stub = false;

	public JsonWatcher(JsonDatabase<T> jsonDatabase, bool stub)
	{
		_jsonDatabaseInstance = jsonDatabase;
		_stub = stub;
	}

	public JsonWatcher(JsonDatabase<T> jsonDatabase)
	{
		LogManager.Info($"[JsonWatcher] \"{jsonDatabase.Name}\": Initializing...");

		_jsonDatabaseInstance = jsonDatabase;

		try
		{
			_watcher = new FileSystemWatcher(jsonDatabase.FilePath);

			_watcher.NotifyFilter = NotifyFilters.Attributes
									| NotifyFilters.CreationTime
									| NotifyFilters.FileName
									| NotifyFilters.LastWrite
									| NotifyFilters.Security
									| NotifyFilters.Size;

			_watcher.Changed += OnJsonFileChanged;
			_watcher.Renamed += OnJsonFileRenamed;
			_watcher.Deleted += OnJsonFileDeleted;
			_watcher.Error += OnJsonFileError;

			_watcher.Filter = $"{jsonDatabase.Name}.json";
			_watcher.EnableRaisingEvents = true;

			LogManager.Info($"[JsonWatcher] \"{jsonDatabase.Name}\": Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~JsonWatcher()
	{
		Dispose();
	}

	public void Enable()
	{
		_disabled = false;
		_delayedEnableTimer?.Dispose();
		_delayedEnableTimer = null;

		if(!_stub) LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Enabled!");
	}

	public void DelayedEnable()
	{
		_delayedEnableTimer?.Dispose();
		_delayedEnableTimer = Timers.SetTimeout(Enable, Constants.ReenableWatcherDelayMilliseconds);

		if(!_stub) LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Will enable after a delay...");
	}

	public void Disable()
	{
		_disabled = true;
		_delayedEnableTimer?.Dispose();

		if(!_stub) LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Temporarily disabled!");
	}

	public void Dispose()
	{
		if(!_stub) LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Disposing...");

		_watcher?.Dispose();

		if(!_stub) LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Disposed!");
	}

	private void OnJsonFileChanged(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(eventTime.Ticks - _lastEventTime.Ticks < Constants.DuplicateEventThresholdTicks) return;

			LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}.json\": Changed.");

			_jsonDatabaseInstance.Load();
			_jsonDatabaseInstance.EmitChanged();

			_lastEventTime = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	// This never gets called because the file is created before the watcher

	//private void OnJsonFileCreated(object? sender, FileSystemEventArgs e)
	//{
	//	try
	//	{
	//		if(_disabled) return;
	//		LogManager.Info($"File \"{jsonDatabaseInstance.Name}\": Created.");

	//		jsonDatabaseInstance.Load();
	//		jsonDatabaseInstance.EmitCreated();
	//	}
	//	catch(Exception exception)
	//	{
	//		LogManager.Error(exception);
	//	}
	//}

	private void OnJsonFileRenamed(object? sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{e.OldName}\": Renamed to \"{e.Name}\".");

			if(e.Name != _watcher?.Filter)
			{
				_jsonDatabaseInstance.EmitRenamedFrom();
			}
			else
			{
				_jsonDatabaseInstance.EmitRenamedTo();
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnJsonFileDeleted(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Deleted.");

			_jsonDatabaseInstance.EmitDeleted();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnJsonFileError(object? sender, ErrorEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{_jsonDatabaseInstance.Name}\": Unknown error.");

			_jsonDatabaseInstance.Load();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}