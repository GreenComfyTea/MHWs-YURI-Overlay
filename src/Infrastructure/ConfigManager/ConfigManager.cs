namespace YURI_Overlay;

internal sealed partial class ConfigManager : IDisposable
{
	private static readonly Lazy<ConfigManager> Lazy = new(() => new ConfigManager());
	public static ConfigManager Instance => Lazy.Value;

	public ConfigCustomization Customization;

	public JsonDatabase<Config> ActiveConfig;
	public Config DefaultConfig;
	public Dictionary<string, JsonDatabase<Config>> Configs = [];

	public EventHandler ActiveConfigChanged = delegate { };
	public EventHandler AnyConfigChanged = delegate { };

	public ConfigWatcher ConfigWatcherInstance;

	private JsonDatabase<CurrentConfig> _currentConfigInstance;

	private ConfigManager() { }

	~ConfigManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("[ConfigManager] Initializing...");


		InitializeDefaultConfig();
		LoadAllConfigs();
		LoadCurrentConfig();

		ConfigWatcherInstance = new ConfigWatcher();
		Customization = new ConfigCustomization();

		LogManager.Info("[ConfigManager] Initialized!");
	}

	public void ActivateConfig(JsonDatabase<Config> config)
	{
		LogManager.Info($"[ConfigManager] Activating config \"{config.Name}\"...");

		ActiveConfig = config;
		_currentConfigInstance.Data.config = config.Name;
		_currentConfigInstance.Save();

		EmitActiveConfigChanged();

		LogManager.Info($"[ConfigManager] Config \"{config.Name}\" activated!");
	}

	public void ActivateConfig(string name)
	{
		LogManager.Info($"[ConfigManager] Searching for config \"{name}\" to activate it...");

		var isGetConfigSuccess = Configs.TryGetValue(name, out var config);

		if(!isGetConfigSuccess)
		{
			LogManager.Info($"[ConfigManager] Config \"{name}\" is not found. ...");
			LogManager.Info("[ConfigManager] Searching for default config to activate it...");

			var isGetDefaultConfigSuccess = Configs.TryGetValue(Constants.DefaultConfig, out var defaultConfig);

			if(!isGetDefaultConfigSuccess)
			{
				LogManager.Info("[ConfigManager] Default config is not found. Creating it...");

				var newDefaultConfig = InitializeConfig(Constants.DefaultConfig);
				ResetToDefault(newDefaultConfig);

				LogManager.Info("[ConfigManager] Default config created!");

				ActivateConfig(newDefaultConfig);
				return;
			}

			LogManager.Info("[ConfigManager] Default config found!");

			ActivateConfig(defaultConfig);
			return;
		}

		LogManager.Info($"[ConfigManager] Config \"{name}\" found!");

		ActivateConfig(config);
	}

	public JsonDatabase<Config> InitializeConfig(string name, Config configToClone = null)
	{
		LogManager.Info($"[ConfigManager] Initializing config \"{name}\"...");

		JsonDatabase<Config> config = new(Constants.ConfigsPath, name, configToClone);
		//if(configToClone is null) DefaultConfig.ResetTo(config.data);
		config.Save();

		config.Changed += OnConfigFileChanged;
		config.RenamedFrom += OnConfigFileRenamedFrom;
		config.RenamedTo += OnConfigFileRenamedTo;
		config.Deleted += OnConfigFileDeleted;
		config.Error += OnConfigFileError;

		Configs[name] = config;

		EmitAnyConfigChanged();

		LogManager.Info($"[ConfigManager] Config \"{name}\" initialized!");

		return config;
	}

	public void NewConfig(string newConfigName)
	{
		ConfigWatcherInstance.Disable();
		var newConfig = InitializeConfig(newConfigName);
		ResetToDefault(newConfig);
		ConfigWatcherInstance.DelayedEnable();

		ActivateConfig(newConfig);
	}

	public void DuplicateConfig(string newConfigName)
	{
		ConfigWatcherInstance.Disable();

		var newConfig = InitializeConfig(newConfigName, ActiveConfig.Data);
		ConfigWatcherInstance.DelayedEnable();

		ActivateConfig(newConfig);
	}

	public void RenameConfig(string newConfigName)
	{
		ConfigWatcherInstance.Disable();

		var oldConfig = ActiveConfig;
		var newConfig = InitializeConfig(newConfigName, ActiveConfig.Data);

		ActivateConfig(newConfig);
		Configs.Remove(oldConfig.Name);
		oldConfig.Delete();

		ConfigWatcherInstance.DelayedEnable();

		Utils.EmitEvents(this, AnyConfigChanged);
	}

	public void ResetConfig()
	{
		ResetToDefault(ActiveConfig);
	}

	public void Dispose()
	{
		LogManager.Info("[ConfigManager] Disposing...");

		ConfigWatcherInstance.Dispose();
		_currentConfigInstance.Dispose();

		foreach(var config in Configs)
		{
			config.Value.Dispose();
		}

		LogManager.Info("[ConfigManager] Disposed!");
	}
	public void EmitAnyConfigChanged()
	{
		Utils.EmitEvents(this, AnyConfigChanged);
	}

	private void InitializeDefaultConfig()
	{
		LogManager.Info("[ConfigManager] Initializing default config reference...");

		DefaultConfig = new Config();
		ResetToDefault(DefaultConfig);

		LogManager.Info("[ConfigManager] Default config reference initialized!");
	}

	private void LoadCurrentConfig()
	{
		LogManager.Info("[ConfigManager] Loading current config...");

		_currentConfigInstance = new JsonDatabase<CurrentConfig>(Constants.PluginDataPath, Constants.CurrentConfig);

		_currentConfigInstance.Changed += OnCurrentConfigChanged;
		_currentConfigInstance.RenamedFrom += OnCurrentConfigRenamedFrom;
		_currentConfigInstance.RenamedTo += OnCurrentConfigRenamedTo;
		_currentConfigInstance.Deleted += OnCurrentConfigDeleted;
		_currentConfigInstance.Error += OnCurrentConfigError;

		ActivateConfig(_currentConfigInstance.Data.config);

		LogManager.Info("[ConfigManager] Current config loaded!");
	}

	private void LoadAllConfigs()
	{
		try
		{
			LogManager.Info("[ConfigManager] Loading all configs...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.ConfigsPath)!);

			var allConfigFilePathNames = Directory.GetFiles(Constants.ConfigsPath);

			if (allConfigFilePathNames.Length == 0)
			{
				var defaultConfig = InitializeConfig(Constants.DefaultConfig);
				ResetToDefault(defaultConfig);
				return;
			}

			foreach (var configFilePathName in allConfigFilePathNames)
			{
				var name = Path.GetFileNameWithoutExtension(configFilePathName);
				InitializeConfig(name);
			}

			LogManager.Info("[ConfigManager] Loading all configs done!");
		}
		catch (Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnCurrentConfigChanged(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file changed.");
		ActivateConfig(_currentConfigInstance.Data.config);
	}

	private void OnCurrentConfigCreated(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file created.");
		_currentConfigInstance.Load();
		ActivateConfig(_currentConfigInstance.Data.config);
	}

	private void OnCurrentConfigRenamedFrom(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file renamed from.");
		_currentConfigInstance.Save();
	}

	private void OnCurrentConfigRenamedTo(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file renamed to.");
		_currentConfigInstance.Load();
		ActivateConfig(_currentConfigInstance.Data.config);
	}

	private void OnCurrentConfigDeleted(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file deleted.");
		_currentConfigInstance.Save();
	}

	private void OnCurrentConfigError(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Current config file throw an error.");
		_currentConfigInstance.Save();
	}

	private void OnConfigFileChanged(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file changed.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileCreated(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file created.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedFrom(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file renamed from.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedTo(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file renamed to.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileDeleted(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file deleted.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileError(object sender, EventArgs eventArgs)
	{
		LogManager.Info("[ConfigManager] Config file throw an error.");
	}

	private void EmitActiveConfigChanged()
	{
		Utils.EmitEvents(this, ActiveConfigChanged);
	}
}
