using Timer = System.Timers.Timer;

namespace YURI_Overlay;

internal sealed class JsonWatcher<T> : IDisposable
	where T : class, new()
{
	private readonly JsonDatabase<T> _jsonDatabaseInstance;

	private readonly bool _stub;
	private readonly FileSystemWatcher? _watcher;
	private Timer? _delayedEnableTimer;

	private bool _disabled;
	private DateTime _lastEventTime = DateTime.MinValue;

	public JsonWatcher(JsonDatabase<T> jsonDatabase, bool stub)
	{
		this._jsonDatabaseInstance = jsonDatabase;
		this._stub = stub;
	}

	public JsonWatcher(JsonDatabase<T> jsonDatabase)
	{
		LogManager.Info($"[JsonWatcher] \"{jsonDatabase.name}\": Initializing...");

		this._jsonDatabaseInstance = jsonDatabase;

		try
		{
			this._watcher = new FileSystemWatcher(jsonDatabase.filePath);

			this._watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;

			this._watcher.Changed += this.OnJsonFileChanged;
			this._watcher.Renamed += this.OnJsonFileRenamed;
			this._watcher.Deleted += this.OnJsonFileDeleted;
			this._watcher.Error += this.OnJsonFileError;

			this._watcher.Filter = $"{jsonDatabase.name}.json";
			this._watcher.EnableRaisingEvents = true;

			LogManager.Info($"[JsonWatcher] \"{jsonDatabase.name}\": Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		if(!this._stub)
		{
			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Disposing...");
		}

		this._delayedEnableTimer?.Dispose();
		this._watcher?.Dispose();

		if(!this._stub)
		{
			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Disposed!");
		}
	}

	~JsonWatcher()
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
			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Enabled!");
		}
	}

	public void DelayedEnable()
	{
		this._delayedEnableTimer?.Dispose();
		this._delayedEnableTimer = Timers.SetTimeout(this.Enable, Constants.REENABLE_WATCHER_DELAY_MILLISECONDS);

		if(!this._stub)
		{
			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Will enable after a delay...");
		}
	}

	public void Disable()
	{
		this._disabled = true;
		this._delayedEnableTimer?.Dispose();

		if(!this._stub)
		{
			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Temporarily disabled!");
		}
	}

	private void OnJsonFileChanged(object? sender, FileSystemEventArgs e)
	{
		try
		{
			if(this._disabled)
			{
				return;
			}

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(eventTime.Ticks - this._lastEventTime.Ticks < Constants.DUPLICATE_EVENT_THRESHOLD_TICKS)
			{
				return;
			}

			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}.json\": Changed.");

			this._jsonDatabaseInstance.Load();
			this._jsonDatabaseInstance.EmitChanged();

			this._lastEventTime = eventTime;
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
			if(this._disabled)
			{
				return;
			}

			LogManager.Info($"[JsonWatcher] File \"{e.OldName}\": Renamed to \"{e.Name}\".");

			if(e.Name != this._watcher?.Filter)
			{
				this._jsonDatabaseInstance.EmitRenamedFrom();
			}
			else
			{
				this._jsonDatabaseInstance.EmitRenamedTo();
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
			if(this._disabled)
			{
				return;
			}

			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Deleted.");

			this._jsonDatabaseInstance.EmitDeleted();
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
			if(this._disabled)
			{
				return;
			}

			LogManager.Info($"[JsonWatcher] File \"{this._jsonDatabaseInstance.name}\": Unknown error.");

			this._jsonDatabaseInstance.Load();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}