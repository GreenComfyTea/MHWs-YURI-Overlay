namespace YURI_Overlay;

internal sealed partial class ConfigManager : IDisposable
{
	private static readonly Lazy<ConfigManager> _lazy = new(() => new ConfigManager());

	private JsonDatabase<CurrentConfig> _currentConfigInstance;
	public JsonDatabase<Config> ActiveConfig;

	public Dictionary<string, JsonDatabase<Config>> Configs;

	public ConfigWatcher ConfigWatcherInstance;

	public ConfigCustomization Customization;

	public Config DefaultConfig;

	private ConfigManager()
	{
		this.Customization = new ConfigCustomization(true);
		this.DefaultConfig = new Config();
		this.ActiveConfig = new JsonDatabase<Config>(true);
		this.Configs = [];
		this.ConfigWatcherInstance = new ConfigWatcher(true);
		this._currentConfigInstance = new JsonDatabase<CurrentConfig>(true);
	}

	public static ConfigManager Instance => _lazy.Value;

	public void Dispose()
	{
		LogManager.Info("[ConfigManager] Disposing...");

		this.ConfigWatcherInstance.Dispose();
		this._currentConfigInstance.Dispose();

		foreach(var config in this.Configs)
		{
			config.Value.Dispose();
		}

		LogManager.Info("[ConfigManager] Disposed!");
	}

	public event EventHandler ActiveConfigChanged = delegate { };
	public event EventHandler AnyConfigChanged = delegate { };

	~ConfigManager()
	{
		this.Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[ConfigManager] Initializing...");

		this.InitializeDefaultConfig();
		this.LoadAllConfigs();
		this.LoadCurrentConfig();

		this.ConfigWatcherInstance.Dispose();

		this.ConfigWatcherInstance = new ConfigWatcher();
		this.Customization = new ConfigCustomization();

		LogManager.Info("[ConfigManager] Initialized!");
	}

	public void ActivateConfig(JsonDatabase<Config>? config)
	{
		if(config is null) return;

		LogManager.Info($"[ConfigManager] Activating config \"{config.Name}\"...");

		this.ActiveConfig = config;

		this._currentConfigInstance.Data.Config = config.Name;
		this._currentConfigInstance.Save();

		this.EmitActiveConfigChanged();

		LogManager.Info($"[ConfigManager] Config \"{config.Name}\" activated!");
	}

	public void ActivateConfig(string name)
	{
		LogManager.Info($"[ConfigManager] Searching for config \"{name}\" to activate it...");

		var isGetConfigSuccess = this.Configs.TryGetValue(name, out var config);

		if(!isGetConfigSuccess)
		{
			LogManager.Info($"[ConfigManager] Config \"{name}\" is not found. ...");
			LogManager.Info("[ConfigManager] Searching for default config to activate it...");

			var isGetDefaultConfigSuccess = this.Configs.TryGetValue(Constants.DEFAULT_CONFIG, out var defaultConfig);

			if(!isGetDefaultConfigSuccess)
			{
				LogManager.Info("[ConfigManager] Default config is not found. Creating it...");

				var newDefaultConfig = this.InitializeConfig(Constants.DEFAULT_CONFIG);
				ResetToDefault(newDefaultConfig);

				LogManager.Info("[ConfigManager] Default config created!");

				this.ActivateConfig(newDefaultConfig);

				return;
			}

			LogManager.Info("[ConfigManager] Default config found!");

			this.ActivateConfig(defaultConfig);

			return;
		}

		LogManager.Info($"[ConfigManager] Config \"{name}\" found!");

		this.ActivateConfig(config);
	}

	public JsonDatabase<Config> InitializeConfig(string name, Config? configToClone = null)
	{
		LogManager.Info($"[ConfigManager] Initializing config \"{name}\"...");

		JsonDatabase<Config> config = new(Constants.CONFIGS_PATH, name, configToClone);
		MergeConfig(config.Data, this.DefaultConfig);
		//if(configToClone is null) DefaultConfig.ResetTo(config.data);
		config.Save();

		config.Changed += this.OnConfigFileChanged;
		config.RenamedFrom += this.OnConfigFileRenamedFrom;
		config.RenamedTo += this.OnConfigFileRenamedTo;
		config.Deleted += this.OnConfigFileDeleted;
		config.Error += this.OnConfigFileError;

		this.Configs[name] = config;

		this.EmitAnyConfigChanged();

		LogManager.Info($"[ConfigManager] Config \"{name}\" initialized!");

		return config;
	}

	public void NewConfig(string newConfigName)
	{
		this.ConfigWatcherInstance.Disable();
		var newConfig = this.InitializeConfig(newConfigName);
		ResetToDefault(newConfig);
		this.ConfigWatcherInstance.DelayedEnable();

		this.ActivateConfig(newConfig);
	}

	public void DuplicateConfig(string newConfigName)
	{
		this.ConfigWatcherInstance.Disable();

		var newConfig = this.InitializeConfig(newConfigName, this.ActiveConfig.Data);
		this.ConfigWatcherInstance.DelayedEnable();

		this.ActivateConfig(newConfig);
	}

	public void RenameConfig(string newConfigName)
	{
		this.ConfigWatcherInstance.Disable();

		var oldConfig = this.ActiveConfig;
		var newConfig = this.InitializeConfig(newConfigName, this.ActiveConfig.Data);

		this.ActivateConfig(newConfig);
		this.Configs.Remove(oldConfig.Name);
		oldConfig.Delete();

		this.ConfigWatcherInstance.DelayedEnable();

		Utils.EmitEvents(this, this.AnyConfigChanged);
	}

	public void ResetConfig()
	{
		ResetToDefault(this.ActiveConfig);
	}

	public void EmitAnyConfigChanged()
	{
		Utils.EmitEvents(this, this.AnyConfigChanged);
	}

	private void InitializeDefaultConfig()
	{
		LogManager.Info("[ConfigManager] Initializing default config reference...");

		this.DefaultConfig = new Config();
		ResetToDefault(this.DefaultConfig);

		LogManager.Info("[ConfigManager] Default config reference initialized!");
	}

	private void LoadCurrentConfig()
	{
		LogManager.Info("[ConfigManager] Loading current config...");

		this._currentConfigInstance = new JsonDatabase<CurrentConfig>(Constants.PLUGIN_DATA_PATH, Constants.CURRENT_CONFIG);

		this._currentConfigInstance.Changed += this.OnCurrentConfigChanged;
		this._currentConfigInstance.RenamedFrom += this.OnCurrentConfigRenamedFrom;
		this._currentConfigInstance.RenamedTo += this.OnCurrentConfigRenamedTo;
		this._currentConfigInstance.Deleted += this.OnCurrentConfigDeleted;
		this._currentConfigInstance.Error += this.OnCurrentConfigError;

		this.ActivateConfig(this._currentConfigInstance.Data.Config);

		LogManager.Info("[ConfigManager] Current config loaded!");
	}

	private void LoadAllConfigs()
	{
		try
		{
			LogManager.Info("[ConfigManager] Loading all configs...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.CONFIGS_PATH)!);

			var allConfigFilePathNames = Directory.GetFiles(Constants.CONFIGS_PATH);

			if(allConfigFilePathNames.Length == 0)
			{
				var defaultConfig = this.InitializeConfig(Constants.DEFAULT_CONFIG);
				ResetToDefault(defaultConfig);

				return;
			}

			foreach(var configFilePathName in allConfigFilePathNames)
			{
				var name = Path.GetFileNameWithoutExtension(configFilePathName);
				this.InitializeConfig(name);
			}

			LogManager.Info("[ConfigManager] Loading all configs done!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnCurrentConfigChanged(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file changed.");
		this.ActivateConfig(this._currentConfigInstance.Data.Config);
	}

	private void OnCurrentConfigCreated(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file created.");

		this._currentConfigInstance.Load();
		this.ActivateConfig(this._currentConfigInstance.Data.Config);
	}

	private void OnCurrentConfigRenamedFrom(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file renamed from.");
		this._currentConfigInstance.Save();
	}

	private void OnCurrentConfigRenamedTo(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file renamed to.");

		this._currentConfigInstance.Load();
		this.ActivateConfig(this._currentConfigInstance.Data.Config);
	}

	private void OnCurrentConfigDeleted(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file deleted.");
		this._currentConfigInstance.Save();
	}

	private void OnCurrentConfigError(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file throw an error.");
		this._currentConfigInstance.Save();
	}

	private void OnConfigFileChanged(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file changed.");
		this.EmitAnyConfigChanged();
	}

	private void OnConfigFileCreated(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file created.");
		this.EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedFrom(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file renamed from.");
		this.EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedTo(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file renamed to.");
		this.EmitAnyConfigChanged();
	}

	private void OnConfigFileDeleted(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file deleted.");
		this.EmitAnyConfigChanged();
	}

	private void OnConfigFileError(object? sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file throw an error.");
	}

	private void EmitActiveConfigChanged()
	{
		Utils.EmitEvents(this, this.ActiveConfigChanged);
	}
}