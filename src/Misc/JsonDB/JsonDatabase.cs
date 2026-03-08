using System.Text.Json;

namespace YURI_Overlay;

internal sealed class JsonDatabase<T> : IDisposable
	where T : class, new()
{
	private readonly bool _stub;
	private readonly FileSync _fileSync;
	private readonly JsonWatcher<T> _jsonWatcher;

	public readonly string name;
	public readonly string filePath;

	public T data;

	public JsonDatabase(bool stub)
	{
		this._stub = stub;

		this.name = string.Empty;
		this.filePath = string.Empty;

		this._fileSync = new FileSync(string.Empty);
		this._jsonWatcher = new JsonWatcher<T>(this, true);

		this.data = new T();
	}

	public JsonDatabase(string path, string name = Constants.PLUGIN_DATA_PATH, T? data = null)
	{
		this.name = name;
		this.filePath = path;

		var filePathName = Path.Combine(path, $"{name}.json");
		this._fileSync = new FileSync(filePathName);

		this._jsonWatcher = new JsonWatcher<T>(this);

		try
		{
			this.data = this.Load(data);
		}
		catch(Exception exception)
		{
			this.data = new T();
			LogManager.Error(exception);
		}
	}

	~JsonDatabase()
	{
		this.Dispose();
	}

	public void Dispose()
	{
		if(!this._stub)
		{
			LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Disposing...");
		}

		this._jsonWatcher.Dispose();

		if(!this._stub)
		{
			LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Disposed!");
		}
	}

	public event EventHandler Changed = delegate { };
	public event EventHandler Renamed = delegate { };
	public event EventHandler RenamedFrom = delegate { };
	public event EventHandler RenamedTo = delegate { };
	public event EventHandler Deleted = delegate { };
	public event EventHandler Error = delegate { };

	public T Load(T? loadData = null)
	{
		try
		{
			this._jsonWatcher.Disable();

			if(!this._stub)
			{
				LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Loading... {loadData}");
			}

			var json = loadData is null ? this._fileSync.Read() : JsonSerializer.Serialize(loadData, Constants.jsonSerializerOptionsInstanceS);

			if(json is null)
			{
				throw new Exception($"[JsonDatabase] File \"{this.name}.json\": Read() returned null!");
			}

			var newData = JsonSerializer.Deserialize<T>(json, Constants.jsonSerializerOptionsInstanceS);

			if(newData is null)
			{
				throw new ArgumentNullException($"[JsonDatabase] File \"{this.name}.json\": Deserialized data is null!");
			}

			this._fileSync.Write(json);
			this.data = newData;

			if(!this._stub)
			{
				LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Loaded!");
			}

			this._jsonWatcher.DelayedEnable();

			return this.data;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			this.data = new T();
			this.Save();

			return this.data;
		}
	}

	public bool Save()
	{
		try
		{
			if(!this._stub)
			{
				LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Saving...");
			}

			this._jsonWatcher.Disable();

			var json = JsonSerializer.Serialize(this.data, Constants.jsonSerializerOptionsInstanceS);

			var isSuccess = this._fileSync.Write(json);

			if(isSuccess)
			{
				if(!this._stub)
				{
					LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Saved!");
				}
			}
			else
			{
				if(!this._stub)
				{
					LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Saving failed!");
				}
			}

			this._jsonWatcher.DelayedEnable();

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
		if(!this._stub)
		{
			LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Deleting...");
		}

		this.Dispose();
		this._fileSync.Delete();

		if(!this._stub)
		{
			LogManager.Info($"[JsonDatabase] File \"{this.name}.json\": Deleted!");
		}
	}

	public void EmitChanged()
	{
		Utils.EmitEvents(this, this.Changed);
	}

	public void EmitRenamedFrom()
	{
		Utils.EmitEvents(this, this.RenamedFrom);
		Utils.EmitEvents(this, this.Renamed);
	}

	public void EmitRenamedTo()
	{
		Utils.EmitEvents(this, this.RenamedTo);
		Utils.EmitEvents(this, this.Renamed);
	}

	public void EmitDeleted()
	{
		Utils.EmitEvents(this, this.Deleted);
	}

	private void OnError()
	{
		Utils.EmitEvents(this, this.Error);
	}
}