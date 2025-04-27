using System.Text.Json;

namespace YURI_Overlay;

internal sealed class JsonDatabase<T> : IDisposable where T : class, new()
{
	public string Name = string.Empty;
	public string FilePath = Constants.PluginDataPath;

	public T Data;
	public FileSync FileSyncInstance;
	public JsonWatcher<T> JsonWatcherInstance;

	public EventHandler Changed = delegate { };
	public EventHandler Renamed = delegate { };
	public EventHandler RenamedFrom = delegate { };
	public EventHandler RenamedTo = delegate { };
	public EventHandler Deleted = delegate { };
	public EventHandler Error = delegate { };

	public JsonDatabase(string path, string name, T data = null)
	{
		try
		{
			Name = name;
			FilePath = path;

			var filePathName = Path.Combine(path, $"{name}.json");
			FileSyncInstance = new FileSync(filePathName);

			Load(data);

			JsonWatcherInstance = new JsonWatcher<T>(this);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~JsonDatabase()
	{
		Dispose();
	}

	public T Load(T data = null)
	{
		try
		{
			JsonWatcherInstance?.Disable();
			LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Loading... ${data}");

			var json = data is null ? FileSyncInstance.Read() : JsonSerializer.Serialize(data, Constants.JsonSerializerOptionsInstance);

			Data = JsonSerializer.Deserialize<T>(json, Constants.JsonSerializerOptionsInstance);
			FileSyncInstance.Write(json);

			LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Loaded!");
			JsonWatcherInstance?.DelayedEnable();
			return Data;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			Data = new T();
			Save();
			return Data;
		}
	}

	public bool Save()
	{
		try
		{
			LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Saving...");
			JsonWatcherInstance?.Disable();

			var json = JsonSerializer.Serialize(Data, Constants.JsonSerializerOptionsInstance);

			var isSuccess = FileSyncInstance.Write(json);

			if(isSuccess)
				LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Saved!");
			else
				LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Saving failed!");

			JsonWatcherInstance?.DelayedEnable();
			return isSuccess;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return false;
		}
	}

	public void Delete()
	{
		LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Deleting...");
		Dispose();
		FileSyncInstance.Delete();
		LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Deleted!");
	}

	public void EmitChanged()
	{
		Utils.EmitEvents(this, Changed);
	}

	public void EmitRenamedFrom()
	{
		Utils.EmitEvents(this, RenamedFrom);
		Utils.EmitEvents(this, Renamed);
	}

	public void EmitRenamedTo()
	{
		Utils.EmitEvents(this, RenamedTo);
		Utils.EmitEvents(this, Renamed);
	}

	public void EmitDeleted()
	{
		Utils.EmitEvents(this, Deleted);
	}

	public void Dispose()
	{
		LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Disposing...");

		JsonWatcherInstance.Dispose();

		LogManager.Info($"[JsonDatabase] File \"{Name}.json\": Disposed!");
	}
}