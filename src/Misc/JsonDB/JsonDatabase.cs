using System.Text.Json;

namespace YURI_Overlay;

internal sealed class JsonDatabase<T> : IDisposable
	where T : class, new()
{
	private readonly FileSync _fileSync;
	private readonly JsonWatcher<T> _jsonWatcher;
	private readonly bool _stub;
	public readonly string FilePath;

	public readonly string Name;

	public T Data;

	public JsonDatabase(bool stub)
	{
		this._stub = stub;

		this.Name = string.Empty;
		this.FilePath = string.Empty;

		this._fileSync = new FileSync(string.Empty);
		this._jsonWatcher = new JsonWatcher<T>(this, true);

		this.Data = new T();
	}

	public JsonDatabase(string path, string name = Constants.PLUGIN_DATA_PATH, T? data = null)
	{
		this.Name = name;
		this.FilePath = path;

		var filePathName = Path.Combine(path, $"{name}.json");
		this._fileSync = new FileSync(filePathName);

		this._jsonWatcher = new JsonWatcher<T>(this);

		try
		{
			this.Data = this.Load(data);
		}
		catch(Exception exception)
		{
			this.Data = new T();
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Disposing...");

		this._jsonWatcher.Dispose();

		if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Disposed!");
	}

	~JsonDatabase()
	{
		this.Dispose();
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

			if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Loading... {loadData}");

			var json = loadData is null ? this._fileSync.Read() : JsonSerializer.Serialize(loadData, Constants.JsonSerializerOptionsInstance);

			if(json is null) throw new Exception($"[JsonDatabase] File \"{this.Name}.json\": Read() returned null!");

			var newData = JsonSerializer.Deserialize<T>(json, Constants.JsonSerializerOptionsInstance);

			if(newData is null) throw new ArgumentNullException($"[JsonDatabase] File \"{this.Name}.json\": Deserialized data is null!");

			this._fileSync.Write(json);
			this.Data = newData;

			if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Loaded!");

			this._jsonWatcher.DelayedEnable();

			return this.Data;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			this.Data = new T();
			this.Save();

			return this.Data;
		}
	}

	public bool Save()
	{
		try
		{
			if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Saving...");

			this._jsonWatcher.Disable();

			var json = JsonSerializer.Serialize(this.Data, Constants.JsonSerializerOptionsInstance);

			var isSuccess = this._fileSync.Write(json);

			if(isSuccess)
			{
				if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Saved!");
			}
			else
			{
				if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Saving failed!");
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
		if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Deleting...");

		this.Dispose();
		this._fileSync.Delete();

		if(!this._stub) LogManager.Info($"[JsonDatabase] File \"{this.Name}.json\": Deleted!");
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